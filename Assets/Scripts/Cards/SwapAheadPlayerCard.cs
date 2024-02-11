using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapAheadPlayerCard : Card
{
    int playerPosition;
    int aheadPosition;
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
        SwapPlayerAndAhead();
    }
    protected void SwapPlayerAndAhead() // there has gotta be a better name for this. oh well
    {
        // get player position
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.currentQueue[i].isPlayer == true)
                {
                    playerPosition = i; break;
                }
            }
        }
        // get person ahead of player's position
        aheadPosition = playerPosition - 1;
        // swap player and person ahead of them
        if (aheadPosition >= 0)
        {
            GameManager.instance.queueManager.LudaSwap(playerPosition, aheadPosition);
		}
		else
		{
            GameManager.instance.queueManager.Fizzle();
        }
    }
}
