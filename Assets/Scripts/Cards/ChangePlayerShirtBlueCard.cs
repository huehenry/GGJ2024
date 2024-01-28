using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerShirtBlueCard : Card
{
    int playerPosition;
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
        ChangePlayerShirtToBlue();
    }

    protected void ChangePlayerShirtToBlue()
    {
        // get player position
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.currentQueue[i].isPlayer == true)
                {
                    GameManager.instance.queueManager.currentQueue[playerPosition].shirt = ShirtType.BLUE; break;
                }
            }
        }
    }
}
