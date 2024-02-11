using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CardInventory : MonoBehaviour
{

    public bool DEBUG;
    public Card[] debugCards;

    public Image loadingScreen;

    public GameObject resetButton;

    public static UI_CardInventory _cardInventory;

    public static Card[] thisLevelCards;
    private List<Card> currentLevelCardInventory;
    public Sprite emptySprite;

    //Used for visuals
    public Image[] cardSprites;
    public TMP_Text[] cardNames;
    //Used for positioning the buttons.
    public RectTransform[] buttonObjects;
    private Button[] buttonsForDisabling;
    //Card visuals will be a child of buttonObjects.
    public RectTransform[] cardVisuals;
    //Private targets for the cards to mvoe towards
    private Vector2[] cardPositions;
    private Quaternion[] cardRotations;
    //Used to know which one is highligted, to mvoe it up and the others down.
    private bool[] currentlyHighlighting;

    public float heightHighlight;
    public float heightUsed;
    public float scaleFactorHighlight;

    //Used for arc
    public float leftLimit = -100;
    public float rightLimit = 100;
    public float leftArcLimit = -5;
    public float rightArcLimit = 5;

    //Used for end.
    public Vector3 cameraPosStart;

    private float animationLerper;

    //Hacky but its a jam
    private int thisCardWasPlayed = -1;
    private Vector2 cardPlayedPos;


    public states cardInventoryStates;
    public enum states
	{
        waiting,
        oldLevelReset,
        newLevelReplace,
        bringBackCards,
        readyToPlayACard,
        waitingForCardToResolve,
        moveCardOffScreen,
        gameDoneCamera,
        fizzleCard,
        finished
	}

    void Awake()
	{
        if(_cardInventory==null)
		{
            _cardInventory = this;
		}
		else
		{
            Debug.Log("WHY IS THERE A DUPLICATE CARD INVENTORY?!?");
            Destroy(this.gameObject);
		}
        cardPositions = new Vector2[buttonObjects.Length];
        cardRotations = new Quaternion[buttonObjects.Length];
        currentlyHighlighting = new bool[buttonObjects.Length];
        buttonsForDisabling = new Button[buttonObjects.Length];
        for(int i = 0; i < buttonsForDisabling.Length; i++)
		{
            buttonsForDisabling[i] = buttonObjects[i].GetComponent<Button>();
		}
	}

    public void Start()
    {
        if (DEBUG == true)
        {
            NewLevel(debugCards);
        }
        foreach (RectTransform r in buttonObjects)
        {
            Vector2 currentPos = r.anchoredPosition;
            currentPos.y = heightUsed;
            r.anchoredPosition = currentPos;
        }
    }

	public void NewLevel(Card[] newLevelCards)
	{
        cameraPosStart = Camera.main.transform.localPosition;
        animationLerper = 0;
        cardInventoryStates = states.oldLevelReset;
        currentLevelCardInventory = new List<Card>();
        foreach(Card c in newLevelCards)
		{
            currentLevelCardInventory.Add(c);
		}

        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlaySound(AudioManager._audioManager.cardShuffle);
        }
        resetButton.SetActive(false);

        // Debug.Log("here");
    }

    public void ResetLevel()
	{
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlayRandomGrumble();
        }

        //This does the same thing as a new level but does not replace the current card inventory
        animationLerper = 0;

        // Reset fizzled cards
        for (int i = 0; i < cardSprites.Length; i++)
        {
            cardSprites[i].transform.localScale = Vector3.one;
        }

        GameLoader.instance.LoadLevel(GameLoader.instance.currentLevel);
	}

    // Update is called once per frame
    void Update()
    {
        if(cardInventoryStates != states.waiting)
		{
            loadingScreen.color = Color.Lerp(loadingScreen.color, new Color(1, 1, 1, 0), Time.deltaTime * 3);
        }
        switch (cardInventoryStates)
        {
            case states.oldLevelReset:
                foreach (RectTransform r in buttonObjects)
                {
                    Vector2 currentPos = r.anchoredPosition;
                    currentPos.y = heightUsed;
                    r.anchoredPosition = currentPos;
                }
                break;
            case states.newLevelReplace:
                //Now replace the cards with the new ones.
                for (int i = 0; i < cardSprites.Length; i++)
                {
                    if (i < currentLevelCardInventory.Count)
                    {
                        cardSprites[i].sprite = currentLevelCardInventory[i].cardSpriteForVisual;
                        cardNames[i].text = currentLevelCardInventory[i].cardNameForVisual;
                    }
                    else
                    {
                        //Set it to an empty sprite. This is a quick and dirty way to know when a card is unused in a level so we don't load it.
                        cardSprites[i].sprite = emptySprite;
                        cardNames[i].text = "UNUSED";
                    }
                }
                cardInventoryStates = states.readyToPlayACard;
                break;
            case states.readyToPlayACard:
                if (resetButton.activeSelf == false)
                {
                    if (GameManager.instance.queueManager.currentQueue.Count > 0)
                    {
                        bool tester = true;
                        foreach (QueuePerson p in GameManager.instance.queueManager.currentQueue)
                        {
                            if (p.move == true)
                            {
                                tester = false;
                            }
                        }
                        if (tester == true)
                        {
                            resetButton.SetActive(true);
                        }
                    }

                }
                //Remaining cards always very quickly center themselves based on how many are left
                int numCardsLeft = 0;
                foreach (Image i in cardSprites)
                {
                    if (i.sprite != emptySprite)
                    {
                        numCardsLeft += 1;
                    }
                }
                //Find the positions by splitting the arc and width by number left.
                float xSplit = rightLimit - leftLimit;
                //An attempt to make it work nicer with fewer and more cards
                float testSpread = 200 * numCardsLeft;
                xSplit += testSpread;
                xSplit /= (numCardsLeft + 1);
                float arcSplit = rightArcLimit - leftArcLimit;
                arcSplit /= (numCardsLeft + 1);
                List<Vector2> newPositions = new List<Vector2>();

                //This allows me to put the correct cards at the right targets;
                int cardCounter = 1;
                for (int i = 0; i < cardSprites.Length; i++)
                {
                    if (cardSprites[i].sprite != emptySprite && i != thisCardWasPlayed)
                    {
                        //This card is still in use
                        cardPositions[i] = new Vector2((cardCounter) * xSplit + leftLimit - testSpread / 2, 0);
                        //Gonna activate it here as well?
                        buttonsForDisabling[i].interactable = true;
                        //HAVE TO INVERSE ROTATION
                        cardRotations[i] = Quaternion.Euler(0, 0, (arcSplit * (cardCounter) + leftArcLimit) * -1);
                        cardCounter += 1;
                    }
                    else
                    {
                        cardPositions[i] = new Vector2(0, heightUsed);
                        cardRotations[i] = Quaternion.Euler(0, 0, 0);
                        buttonsForDisabling[i].interactable = false;
                    }
                }
                //SEND CARDS TO THEIR TARGETS
                for (int i = 0; i < cardPositions.Length; i++)
                {
                    buttonObjects[i].anchoredPosition = Vector2.Lerp(buttonObjects[i].anchoredPosition, cardPositions[i], 3 * Time.deltaTime);
                    buttonObjects[i].localRotation = Quaternion.Lerp(buttonObjects[i].localRotation, cardRotations[i], 3 * Time.deltaTime);
                }

                //Any card that's highighted has the visual (separate from the button) move up and to the middle. The rest move down. If there's more than one, throw a warning to figure out why
                bool foundIt = false;
                for (int i = 0; i < currentlyHighlighting.Length; i++)
                {
                    if (currentlyHighlighting[i] == true)
                    {
                        if (foundIt == false)
                        {
                            foundIt = true;
                        }
                        else
                        {
                            Debug.LogError("More than one card is selected??");
                        }
                        cardVisuals[i].anchoredPosition = new Vector2(0, heightHighlight);
                        cardVisuals[i].localScale = new Vector3(scaleFactorHighlight, scaleFactorHighlight, scaleFactorHighlight);
                    }
                    else
                    {
                        cardVisuals[i].anchoredPosition = new Vector2(0, 0);
                        cardVisuals[i].localScale = Vector3.one;
                    }
                }
                break;
            case states.waitingForCardToResolve:
                //A card was clicked. Don't let the player click another card until whatever has happened.
                foreach (Button b in buttonsForDisabling)
                {
                    b.interactable = false;
                }
                break;
            case states.moveCardOffScreen:
                //The card is resolved. Drop the visual AND the collision away.
                animationLerper += Time.deltaTime * 3;
                float stepLerper = Mathf.SmoothStep(0, 1, animationLerper);
                if (thisCardWasPlayed != -1)
                {
                    cardPositions[thisCardWasPlayed] = new Vector2(0, heightUsed);
                    cardRotations[thisCardWasPlayed] = Quaternion.Euler(0, 0, 0);
                    buttonObjects[thisCardWasPlayed].anchoredPosition = Vector2.Lerp(cardPlayedPos, cardPositions[thisCardWasPlayed], stepLerper);
                }
                //Once lerper is done, set its image and name and then restart
                if (animationLerper > 1)
                {
                    cardSprites[thisCardWasPlayed].sprite = emptySprite;
                    cardNames[thisCardWasPlayed].text = "UNUSED";
                    animationLerper = 0;
                    cardInventoryStates = states.readyToPlayACard;
                    thisCardWasPlayed = -1;
                    //CHECK FOR WIN HERE.
                    if (GameManager.instance.queueManager.checkForWin() == true)
                    {
                        // Play victory jingle
                        if (AudioManager._audioManager != null)
                        {
                            AudioManager._audioManager.PlaySound(AudioManager._audioManager.victory);
                        }

                        //If last level do special thing here.
                        if (GameManager.instance.queueManager.lastLevel == true)
                        {
                            cardInventoryStates = states.gameDoneCamera;
                            animationLerper = 0;
                        }
                        else
                        {
                            loadingScreen.color = Color.white;
                            cardInventoryStates = states.waiting;
                            CutsceneManager._cutsceneManager.PlayCutsceneThenLoadLevel(GameLoader.instance.currentLevel.nextCutscene);
                        }
                    }
                }
                break;
            case states.fizzleCard:
                cardSprites[thisCardWasPlayed].transform.localScale = Vector3.MoveTowards(cardSprites[thisCardWasPlayed].transform.localScale, Vector3.zero, Time.deltaTime);
                if (cardSprites[thisCardWasPlayed].transform.localScale == Vector3.zero)
                {
                    cardInventoryStates = states.moveCardOffScreen;
                }
                break;
            case states.gameDoneCamera:
                animationLerper += Time.deltaTime;
                if (animationLerper <= 3)
                {
                    float smoother = Mathf.SmoothStep(0, 1, animationLerper/2);
                    Vector3 target = cameraPosStart;
                    target.x += 6;
                    Camera.main.transform.localPosition = Vector3.Lerp(cameraPosStart, target, smoother);
				}
				else
				{
                    //When done
                    CutsceneManager._cutsceneManager.PlayCutsceneThenLoadLevel(GameLoader.instance.currentLevel.nextCutscene);
                    cardInventoryStates = states.finished;
                }
                break;
		}
    }

    public void Hovering(int cardNum)
	{
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlaySound(AudioManager._audioManager.beep);
        }

        currentlyHighlighting[cardNum] = true;
	}

    public void ExitHovering(int cardNum)
	{
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlaySound(AudioManager._audioManager.beep);
        }
        currentlyHighlighting[cardNum] = false;
    }

    public void ClickedACard(int cardNum)
	{
        //Do the effect of the card.
        cardInventoryStates = states.waitingForCardToResolve;
        currentLevelCardInventory[cardNum].CallEffect();
        thisCardWasPlayed = cardNum;
        cardPlayedPos = buttonObjects[cardNum].anchoredPosition;

        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlaySound(AudioManager._audioManager.playCard);
        }
        resetButton.SetActive(false);
    }

    public void DoneResolvingCard()
	{
        cardInventoryStates = states.moveCardOffScreen;
    }
}
