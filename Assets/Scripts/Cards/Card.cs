using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour
{
    protected bool isUsed; // bool for checking whether the cards been used this round
    public Sprite cardSpriteForVisual;
    public string cardNameForVisual;

    // protected Image cardImage; // image for the card (forgot how images work in unity so i've commented it out for now)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public abstract void CallEffect(); // function that children can use to call their effects
    public abstract void RefreshCard(); // be able to refresh whether the card can be used (ex. when the level is restarted)
}
