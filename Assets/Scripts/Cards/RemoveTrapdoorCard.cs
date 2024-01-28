using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTrapdoorCard : Card
{
    public List<QueuePerson> removeThese;
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
        RemoveTrapdoors();
    }

    protected void RemoveTrapdoors()
    {
        removeThese = new List<QueuePerson>();
        // get all people on trapdoors
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.trapdoors[i] == true)
                {
                    removeThese.Add(GameManager.instance.queueManager.currentQueue[i]);
                }
            }
        }
        // remove them from queue
        GameManager.instance.queueManager.TrapDoor(removeThese);
    }
}
