using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothesAroundPlayerCard : Card
{
    int playerPosition;
    int aheadPosition;
    int behindPosition;
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
        MatchClothes();
    }
    protected void MatchClothes()
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
        // get person behind player's position
        behindPosition = playerPosition + 1;

        if (aheadPosition < GameManager.instance.queueManager.currentQueue.Count && behindPosition >= 0)
        {
            // change shirt of person ahead
            GameManager.instance.queueManager.currentQueue[aheadPosition].shirt = GameManager.instance.queueManager.currentQueue[playerPosition].shirt;
            // change shirt of person behind
            GameManager.instance.queueManager.currentQueue[behindPosition].shirt = GameManager.instance.queueManager.currentQueue[playerPosition].shirt;

            // i don't know if we want to do pants as well but i'll write it anyways

            // change pants of person ahead
            GameManager.instance.queueManager.currentQueue[aheadPosition].pants = GameManager.instance.queueManager.currentQueue[playerPosition].pants;
            // change pants of person behind
            GameManager.instance.queueManager.currentQueue[behindPosition].pants = GameManager.instance.queueManager.currentQueue[playerPosition].pants;

            //Add them to the list for spinning
            List<QueuePerson> spinMe = new List<QueuePerson>();
            spinMe.Add(GameManager.instance.queueManager.currentQueue[aheadPosition]);
            spinMe.Add(GameManager.instance.queueManager.currentQueue[behindPosition]);
            GameManager.instance.queueManager.UpdateVisuals(spinMe);

        }
		else
		{
            //Fizzle
		}
    }
}