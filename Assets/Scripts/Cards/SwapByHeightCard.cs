using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapByHeightCard : Card
{
    protected int playerPosition;
    protected int otherPosition;

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
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.currentQueue[i].isPlayer == true)
                {
                    playerPosition = i; break;
                    // get their height here
                }
            }
        }
        // get other person with same height? and their position (unclear if this is player choice or if it's just random or what)
            // use the player height to determine the other person
            // get the other person's position
        // swap player and other person
        GameManager.instance.queueManager.Swap(playerPosition,otherPosition);
    }
}
