using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapAheadAndBehindCard : Card
{
    protected int playerPosition;
    protected int aheadPosition;
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
        SwapAheadAndBehind();
    }

    protected void SwapAheadAndBehind()
    {
        // get player's position
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
        // get ahead person's position
        aheadPosition = playerPosition - 1;
        // get behind person's position
        behindPosition = playerPosition + 1;
        // swap ahead and behind
        GameManager.instance.queueManager.Swap(aheadPosition, behindPosition);
    }
}
