using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    //Putting this here
    public GameObject trapdoorPrefab;
    public GameObject[] numbers;

    public List<QueuePerson> currentQueue;
    public Transform[] spawnPositions;
    public List<GameObject> trapdoorObjects;
    public List<bool> trapdoors;
    public float moveSpeed;
    public float swapSpeed;
    public float spinSpeed;
    private float timer;
    private int stagger;

    //These are used to swap
    private QueuePerson swap1;
    private QueuePerson swap2;

    //Used to delete and rejoin
    private List<QueuePerson> tempList;

    private class CandyCrushList
	{
        public QueuePerson one;
        public QueuePerson two;
        public QueuePerson three;
    }

    public enum actionStates
	{
        waiting,
        spawning,
        swapping,
        removal_delete,
        removal_turnTowardsUser,
        removal_moveTowardsUser,
        removal_turnOffscreen,
        removal_moveOffscreen,
        removal_trapdoor,
        removal_moveForward,
        repopulate,
        changingProperty,
        stopSpinning
	}

    public actionStates currentState;

    public void LoadNewQueue(List<QueuePerson> queueToPopulate, List<bool> trapdoorList)
	{
        //Delete old queue
        for(int i = currentQueue.Count-1; i>=0; i--)
		{
            Destroy(currentQueue[i].gameObject);             
		}

        currentQueue = new List<QueuePerson>();
        //Create new list.
        foreach(QueuePerson person in queueToPopulate)
		{
            currentQueue.Add(person);
            person.moveSpeed = moveSpeed;
		}
        //Give them positions
        for (int i = 0; i < currentQueue.Count; i++)
        {
            currentQueue[i].currentTargetPos = spawnPositions[i].localPosition + (Vector3.up * currentQueue[i].yOffset);
        }
        timer = 0;
        stagger = 0;
        currentState = actionStates.spawning;
        //Also add trapdoors
        //Delete old trapdoors if they exist
        for(int i = 0; i < trapdoorObjects.Count; i++)
		{
            Destroy(trapdoorObjects[i].gameObject);
		}
        trapdoorObjects = new List<GameObject>();
        trapdoors = new List<bool>();
        for (int i = 0; i < currentQueue.Count; i++)
		{
            trapdoors.Add(trapdoorList[i]);
            //Spawn a trapdoor if necessary.
            if(trapdoorList[i] == true)
			{
                GameObject newDoor = Instantiate(trapdoorPrefab, spawnPositions[i], true);
                newDoor.transform.localPosition = new Vector3(0, 0.5f, 0);
                trapdoorObjects.Add(newDoor);
            }
		}
        for(int i = 0; i < numbers.Length; i++)
		{
            if(i<currentQueue.Count)
			{
                numbers[i].SetActive(true);
			}
			else
			{
                numbers[i].SetActive(false);
            }
		}
    }

	public void Update()
	{
        switch (currentState)
        {
            case actionStates.swapping:
                //Swapping two positions.
                //Wait for both of the guys to stop moving. Then move on
                if (swap1.move == false && swap2.move == false)
                {
                    currentState = actionStates.waiting;
                    FinishAnAction();
                }
                break;
            case actionStates.spawning:
                //Bring people in staggered
                timer += Time.deltaTime;
                if (timer > 0.35f)
                {
                    if (stagger < currentQueue.Count)
                    {
                        currentQueue[stagger].move = true;
                        stagger += 1;
                        timer = 0;
                    }
                    else
                    {
                        bool done = true;
                        foreach (QueuePerson person in currentQueue)
                        {
                            if (person.move == true)
                            {
                                done = false;
                            }
                        }
                        if (done == true)
                        {
                            currentState = actionStates.waiting;
                            UI_CardInventory._cardInventory.cardInventoryStates = UI_CardInventory.states.newLevelReplace;
                        }
                    }
                }
                break;
            case actionStates.removal_delete:
                //Deleting some people to re-add them after.
                //For now they're popping away so just them really far to the left.
                //This function takes the remaining people and puts them in the correct spot;
                RecalculatePositionsAfterDelete();
                break;
            case actionStates.removal_turnTowardsUser:
                Quaternion target = Quaternion.Euler(0, 180, 0);
                bool turned = true;
                foreach (QueuePerson q in tempList)
                {
                    q.transform.localRotation = Quaternion.RotateTowards(q.transform.localRotation, target, 270 * Time.deltaTime);
                    if (q.transform.localRotation != target)
                    {
                        turned = false;
                    }
                }
                if (turned == true)
                {
                    currentState = actionStates.removal_moveTowardsUser;
                }
                break;
            case actionStates.removal_moveTowardsUser:
                bool finishedForward = true;
                foreach (QueuePerson q in tempList)
                {
                    Vector3 newPos = q.transform.localPosition;
                    newPos.z = -2;
                    q.transform.localPosition = Vector3.MoveTowards(q.transform.localPosition, newPos, 6 * Time.deltaTime);
                    if (q.transform.localPosition != newPos)
                    {
                        finishedForward = false;
                    }
                }
                if (finishedForward == true)
                {
                    currentState = actionStates.removal_turnOffscreen;
                }
                break;
            case actionStates.removal_turnOffscreen:
                Quaternion targetOff = Quaternion.Euler(0, -90, 0);
                bool turnedOffScreen = true;
                foreach (QueuePerson q in tempList)
                {
                    q.transform.localRotation = Quaternion.RotateTowards(q.transform.localRotation, targetOff, 270 * Time.deltaTime);
                    if (q.transform.localRotation != targetOff)
                    {
                        turnedOffScreen = false;
                    }
                }
                if (turnedOffScreen == true)
                {
                    currentState = actionStates.removal_moveOffscreen;
                }
                break;
            case actionStates.removal_moveOffscreen:
                bool finishedOffscreen = true;
                foreach (QueuePerson q in tempList)
                {
                    Vector3 newPos = q.transform.localPosition;
                    newPos.x = -20;
                    q.transform.localPosition = Vector3.MoveTowards(q.transform.localPosition, newPos, 20 * Time.deltaTime);
                    if (q.transform.localPosition != newPos)
                    {
                        finishedOffscreen = false;
                    }
                    else
                    {
                        q.transform.localRotation = Quaternion.Euler(0, 90, 0);
                        newPos.z = 0;
                        q.transform.localPosition = newPos;
                    }
                }
                if (finishedOffscreen == true)
                {
                    RecalculatePositionsAfterDelete();
                }
                break;
            case actionStates.removal_trapdoor:
                //Everyone in the temp file needs to animated downwards. Once they're low enough then we just throw them to the left and its a normal removal
                timer += Time.deltaTime;
                foreach (QueuePerson dropper in tempList)
                {
                    Vector3 newTarget = dropper.transform.localPosition;
                    newTarget.y -= 1;
                    dropper.transform.localPosition = Vector3.Lerp(dropper.transform.localPosition, newTarget, 16 * Time.deltaTime);
                }
                if (timer > 1)
                {
                    foreach (QueuePerson person in tempList)
                    {
                        person.transform.localPosition = new Vector3(-20, 0.5f, person.transform.localPosition.z);
                    }
                    RecalculatePositionsAfterDelete();
                    timer = 0;
                }
                break;
            case actionStates.removal_moveForward:
                //Moving leftovers forward
                //Wait for them all to be done.
                bool doneMoving = true;
                foreach (QueuePerson person in currentQueue)
                {
                    if (person.move == true)
                    {
                        doneMoving = false;
                    }
                }
                if (doneMoving == true)
                {
                    AddStragglersAfterDelete();
                }
                break;
            case actionStates.repopulate:
                //Bringing stragglers in.
                //This code is nearly identical to spawning new people in, but we can use the temporary list, they're the only ones left.
                timer += Time.deltaTime;
                if (timer > 0.35f)
                {
                    if (stagger < tempList.Count)
                    {
                        tempList[stagger].move = true;
                        stagger += 1;
                        timer = 0;
                    }
                    else
                    {
                        bool done = true;
                        foreach (QueuePerson person in currentQueue)
                        {
                            if (person.move == true)
                            {
                                done = false;
                            }
                        }
                        if (done == true)
                        {
                            currentState = actionStates.waiting;
                            FinishAnAction();
                        }
                    }
                }
                break;
            case actionStates.changingProperty:
                    timer += Time.deltaTime;
                    foreach (QueuePerson spinMe in tempList)
                    {
                        Quaternion targetRot = Quaternion.Euler(spinMe.transform.localRotation.eulerAngles.x, spinMe.transform.localRotation.eulerAngles.y + 45.0f, spinMe.transform.localRotation.eulerAngles.z);
                        spinMe.transform.localRotation = Quaternion.RotateTowards(spinMe.transform.localRotation, targetRot, timer*3);
                    }
                    if (timer > 2)
                    {
                        foreach(QueuePerson change in tempList)
						{
                            change.SetVisuals();
						}
                        currentState = actionStates.stopSpinning;
                    }
                break;
            case actionStates.stopSpinning:
                timer -= Time.deltaTime;
                foreach (QueuePerson spinMe in tempList)
                {
                    Quaternion targetRot = Quaternion.Euler(spinMe.transform.localRotation.eulerAngles.x, spinMe.transform.localRotation.eulerAngles.y + 45.0f, spinMe.transform.localRotation.eulerAngles.z);
                    if (timer <= 0.5f)
                    {
                        targetRot = Quaternion.Euler(spinMe.transform.localRotation.eulerAngles.x, 90, spinMe.transform.localRotation.eulerAngles.z);
                        spinMe.transform.localRotation = Quaternion.RotateTowards(spinMe.transform.localRotation, targetRot, timer * 3);
					}
					else
					{
                        spinMe.transform.localRotation = Quaternion.RotateTowards(spinMe.transform.localRotation, targetRot, timer * 3);
                    }
                }
                if (timer <=0)
                {
                    foreach (QueuePerson spinMe in tempList)
                    {
                        spinMe.transform.localRotation = Quaternion.Euler(spinMe.transform.localRotation.eulerAngles.x, 90, spinMe.transform.localRotation.eulerAngles.z);
                    }
                     timer = 0;
                    currentState = actionStates.waiting;
                    FinishAnAction();
                }
                break;
        }

	}

    //USE THIS FOR A CARD
    public void Swap(int index1, int index2)
	{
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlayRandomGrumble();
        }

        swap1 = currentQueue[index1];
        swap2 = currentQueue[index2];
        Vector3 temp = swap2.currentTargetPos;
        swap2.currentTargetPos = swap1.currentTargetPos;
        swap1.currentTargetPos = temp;
        swap1.move = true;
        swap2.move = true;
        swap1.moveSpeed = swapSpeed;
        swap2.moveSpeed = swapSpeed;
        currentQueue[index1] = swap2;
        currentQueue[index2] = swap1;
        currentState = actionStates.swapping;
    }

    public void Fizzle()
	{
        //Do nothing. Just resolve?
        UI_CardInventory._cardInventory.cardInventoryStates = UI_CardInventory.states.fizzleCard;

        // Audio
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlaySound(AudioManager._audioManager.fizzleCard);
        }

    }

    //USE THIS FOR A CARD
    public void Deletion(List<QueuePerson> removeThese)
	{
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlayRandomGrumble();
        }


        //Put them far to the left.
        foreach (QueuePerson person in removeThese)
		{
            currentQueue.Remove(person);
		}
        //Save them to our templist. We'll need them later
        tempList = new List<QueuePerson>();
        foreach(QueuePerson p in removeThese)
		{
            tempList.Add(p);
		}
        currentState = actionStates.removal_turnTowardsUser;
    }

    public void Reversal(List<QueuePerson> removeThese)
    {
        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlayRandomGrumble();
        }


        //Put them far to the left.
        foreach (QueuePerson person in removeThese)
        {
            currentQueue.Remove(person);
        }
        //Save them to our templist. We'll need them later
        tempList = new List<QueuePerson>();
        foreach (QueuePerson p in removeThese)
        {
            tempList.Add(p);
        }
        currentState = actionStates.removal_turnOffscreen;
    }

    public void TrapDoor(List<QueuePerson> removeThese)
    {

        if (AudioManager._audioManager != null)
        {
            AudioManager._audioManager.PlaySound(AudioManager._audioManager.wilheilm);
        }

        //Don't move them yet. Just remove them from the queue
        foreach (QueuePerson person in removeThese)
        {
            currentQueue.Remove(person);
        }
        //Save them to our templist. We'll need them later
        tempList = new List<QueuePerson>();
        foreach (QueuePerson p in removeThese)
        {
            tempList.Add(p);
        }
        timer = 0;
        currentState = actionStates.removal_trapdoor;
    }

    public void RecalculatePositionsAfterDelete()
	{
        //This goes through the current characters left and moves them all at the same time.
        for(int i = 0; i < currentQueue.Count; i++)
		{
            currentQueue[i].currentTargetPos = spawnPositions[i].localPosition + (Vector3.up * currentQueue[i].yOffset);
            currentQueue[i].move = true;
            currentQueue[i].moveSpeed = moveSpeed;
		}
        currentState = actionStates.removal_moveForward;
    }

    public void AddStragglersAfterDelete()
	{
        //Add the stragglers to the queue.
        foreach(QueuePerson straggler in tempList)
		{
            currentQueue.Add(straggler);
		}
        //Give the stragglers their new position. We can do this to everyone and it won't be a problem since people in the queue already are already in place.
        for (int i = 0; i < currentQueue.Count; i++)
        {
            currentQueue[i].currentTargetPos = spawnPositions[i].localPosition + (Vector3.up * currentQueue[i].yOffset);
            currentQueue[i].moveSpeed = moveSpeed;
        }
        //Now we will bring them in.
        stagger = 0;
        timer = 0;
        currentState = actionStates.repopulate;
    }

    public List<QueuePerson> largestGroup()
	{
        List<QueuePerson> returner = new List<QueuePerson>();

        QueuePerson bestStreakStarter = currentQueue[0];
        int currentStreak = 1;
        int bestStreak = 0;

        for(int i = 1; i< currentQueue.Count; i++)
		{
            if(currentQueue[i].shirt == currentQueue[i-1].shirt)
			{
                currentStreak += 1;
                if(currentStreak>bestStreak)
				{
                    bestStreakStarter = currentQueue[(i+1) - currentStreak];
                    bestStreak = currentStreak;
                }
			}
			else
			{
                currentStreak = 1;
			}
		}
        currentStreak = 1;
        for (int i = 1; i < currentQueue.Count; i++)
        {
            if (currentQueue[i].pants == currentQueue[i - 1].pants)
            {
                currentStreak += 1;
                if (currentStreak > bestStreak)
                {
                    bestStreakStarter = currentQueue[(i+1) - currentStreak];
                    bestStreak = currentStreak;
                }
            }
            else
            {
                currentStreak = 1;
            }
        }
        //NOW create an array of this group. Start with the streak starter and add anyone else.
        for(int i = 0; i < currentQueue.Count; i++)
		{
            if (returner.Count > 0)
            {
                if(bestStreak>0)
				{
                    bestStreak -= 1;
                    returner.Add(currentQueue[i]);
				}
			}
			else if (currentQueue[i] == bestStreakStarter)
			{
                returner.Add(currentQueue[i]);
                bestStreak -= 1;
			}
		}

        return returner;
	}


	public void FinishAnAction()
	{
        UI_CardInventory._cardInventory.DoneResolvingCard();
	}



    public int returnPlayerPosition()
	{
        int returner = 0;
        for(int i = 0; i < currentQueue.Count; i++)
		{
            if(currentQueue[i].isPlayer == true)
			{
                returner = i;
			}
		}
        return returner;
	}

    public bool checkForWin()
	{
        bool returner = false;
        if(currentQueue[0].isPlayer == true)
		{
            returner = true;
		}
        return returner;
	}

    public void UpdateVisuals(List<QueuePerson> affectThese)
	{
        tempList = affectThese;
        //Put us in the state to spin people
        timer = 0;
        currentState = actionStates.changingProperty;
	}


    public List<QueuePerson> CandyCrushMe()
	{
        List<QueuePerson> returner = new List<QueuePerson>();
        //Lists of matches
        List<CandyCrushList> currentMatches = new List<CandyCrushList>();
        //Find any groups of three
        for (int i = 0; i <= currentQueue.Count - 3; i++)
		{
            bool shirtsMatch = true;
            bool pantsMatch = true;
            if(currentQueue[i].shirt == currentQueue[i+1].shirt && currentQueue[i].shirt == currentQueue[i+2].shirt)
			{
                //Make sure there isn't one before or after of the same type
                if(i>0)
				{
                    //Check the one before if possible
                    if(currentQueue[i-1].shirt == currentQueue[i].shirt)
					{
                        shirtsMatch = false;
					}                        
				}
                if(i < currentQueue.Count - 3)
				{
                    //Check the one after if possible
                    if (currentQueue[i + 3].shirt == currentQueue[i].shirt)
                    {
                        shirtsMatch = false;
                    }
                }
			}
			else
			{
                shirtsMatch = false;
			}
            //Do the same with pants
            if (currentQueue[i].pants == currentQueue[i + 1].pants && currentQueue[i].pants == currentQueue[i + 2].pants)
            {
                //Make sure there isn't one before or after of the same type
                if (i > 0)
                {
                    //Check the one before if possible
                    if (currentQueue[i - 1].pants == currentQueue[i].pants)
                    {
                        pantsMatch = false;
                    }
                }
                if (i < currentQueue.Count - 3)
                {
                    //Check the one after if possible
                    if (currentQueue[i + 3].pants == currentQueue[i].pants)
                    {
                        pantsMatch = false;
                    }
                }
            }
            else
            {
                pantsMatch = false;
            }
            if(shirtsMatch==true || pantsMatch == true)
			{
                //Add this list.
                CandyCrushList matches = new CandyCrushList();
                matches.one = currentQueue[i];
                matches.two = currentQueue[i+1];
                matches.three = currentQueue[i+2];
                currentMatches.Add(matches);
            }
        }
        Debug.Log("NUMBER OF MATCHES:");
        Debug.Log(currentMatches.Count);
        //Now we have a bunch of candy crush matches.
        //Create a list of  ones not to use
        List<QueuePerson> dontCrush = new List<QueuePerson>();
        //If it contains something that another one contains remove both of their contents.
        for(int i = 0; i < currentMatches.Count-1; i++)
		{
            for(int beginWithThisIndex = i+1; beginWithThisIndex < currentMatches.Count; beginWithThisIndex++)
			{
                //Start with the one we're on. Compare it only to the ones after it, since the ones before it will already have been compared to it.
                if(currentMatches[i].one == currentMatches[beginWithThisIndex].two || currentMatches[i].one == currentMatches[beginWithThisIndex].three)
				{
                    //Remove them all.
                    dontCrush.Add(currentMatches[i].one);
                    dontCrush.Add(currentMatches[i].two);
                    dontCrush.Add(currentMatches[i].three);
                    dontCrush.Add(currentMatches[beginWithThisIndex].one);
                    dontCrush.Add(currentMatches[beginWithThisIndex].two);
                    dontCrush.Add(currentMatches[beginWithThisIndex].three);
                }
                //Compare two to one and three.
                if (currentMatches[i].two == currentMatches[beginWithThisIndex].one || currentMatches[i].two == currentMatches[beginWithThisIndex].three)
                {
                    //Remove them all.
                    dontCrush.Add(currentMatches[i].one);
                    dontCrush.Add(currentMatches[i].two);
                    dontCrush.Add(currentMatches[i].three);
                    dontCrush.Add(currentMatches[beginWithThisIndex].one);
                    dontCrush.Add(currentMatches[beginWithThisIndex].two);
                    dontCrush.Add(currentMatches[beginWithThisIndex].three);
                }
                //Compare three to one and two.
                if (currentMatches[i].three == currentMatches[beginWithThisIndex].one || currentMatches[i].three == currentMatches[beginWithThisIndex].two)
                {
                    //Remove them all.
                    dontCrush.Add(currentMatches[i].one);
                    dontCrush.Add(currentMatches[i].two);
                    dontCrush.Add(currentMatches[i].three);
                    dontCrush.Add(currentMatches[beginWithThisIndex].one);
                    dontCrush.Add(currentMatches[beginWithThisIndex].two);
                    dontCrush.Add(currentMatches[beginWithThisIndex].three);
                }
            }
		}
        //Now we have a list of matches, with people in them, and then a dontcrush list
        //Use them to return the list
        for (int i = 0; i < currentMatches.Count; i++)
        {
            if (!returner.Contains(currentMatches[i].one))
            {
                returner.Add(currentMatches[i].one);
            }
            if (!returner.Contains(currentMatches[i].two))
            {
                returner.Add(currentMatches[i].two);
            }
            if (!returner.Contains(currentMatches[i].three))
            {
                returner.Add(currentMatches[i].three);
            }
        }
        //Remove the dontcrushes
        for(int i = 0; i < dontCrush.Count; i++)
		{
            if(returner.Contains(dontCrush[i]))
			{
                returner.Remove(dontCrush[i]);
			}
		}
        return returner;
	}

}
