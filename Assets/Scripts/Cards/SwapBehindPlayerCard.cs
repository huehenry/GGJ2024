using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBehindPlayer : Card
{
    protected int playerPosition;
    protected int behindPosition;
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
        SwapPlayerAndBehind();
    }

    protected void SwapPlayerAndBehind() // there has gotta be a better name for this. oh well
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
        // get person behind player's position
        behindPosition = playerPosition + 1;
        // swap player and person behind them
        if (behindPosition < GameManager.instance.queueManager.currentQueue.Count)
        {
            GameManager.instance.queueManager.Swap(playerPosition, behindPosition);
		}
		else
		{
            GameManager.instance.queueManager.Fizzle();

        }
    }
}
