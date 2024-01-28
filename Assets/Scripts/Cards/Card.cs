using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour
{
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
}
