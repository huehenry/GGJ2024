using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapByHeightCard : Card
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
            SwapSameHeight();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }

    protected void SwapSameHeight()
    {
        // get player height and position
        
        // get other person with same height? and their position (unclear if this is player choice or if it's just random or what)

        // put player at other person's old position

        // put other person at player's old position
    }
}
