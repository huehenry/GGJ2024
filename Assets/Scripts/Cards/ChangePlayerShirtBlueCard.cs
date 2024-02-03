using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerShirtBlueCard : Card
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
        ChangePlayerShirtToBlue();
    }

    protected void ChangePlayerShirtToBlue()
    {
        // get player position
        List<QueuePerson> spinMe = new List<QueuePerson>();
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.currentQueue[i].isPlayer == true)
                {
                    GameManager.instance.queueManager.currentQueue[i].shirt = ShirtType.BLUE;
                    spinMe.Add(GameManager.instance.queueManager.currentQueue[i]);
                    break;
                    
                }
            }
        }
        GameManager.instance.queueManager.UpdateVisuals(spinMe);
    }
}
