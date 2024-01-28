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
        SwapSameHeight();
    }

    protected void SwapSameHeight()
    {
        // get player height and position
        int playerIndex = GameManager.instance.queueManager.returnPlayerPosition();
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (i!= playerIndex && GameManager.instance.queueManager.currentQueue[i].height == GameManager.instance.queueManager.currentQueue[playerIndex].height)
                {
                    otherPosition = i; break;
                    // get their height here
                }
            }
        }
        GameManager.instance.queueManager.Swap(playerIndex, otherPosition);
    }
}
