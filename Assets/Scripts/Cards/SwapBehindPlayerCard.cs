using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBehindPlayer : Card
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
            SwapPlayerAndBehind();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void SwapPlayerAndBehind() // there has gotta be a better name for this. oh well
    {
        // get player position

        // get person behind player's position

        // remove player and person behind them

        // put player in behind person's old position

        // put behind person in player's old position
    }
}
