using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapAheadPlayerCard : Card
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void CallEffect()
    {
        if (isUsed != true)
        {
            isUsed = true;
            SwapPlayerAndAhead();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void SwapPlayerAndAhead() // there has gotta be a better name for this. oh well
    {
        
    }
}
