using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapClothingBehindPlayerCard : Card
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
        if (isUsed != true)
        {
            isUsed = true;
            SwapPlayerAndBehindClothes();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void SwapPlayerAndBehindClothes() // there has gotta be a better name for this. oh well
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
        // swap clothes between player and person behind them
        // maybe make a new function in QueueManager for this? might be overkill idk

    }
}
