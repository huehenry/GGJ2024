using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEvenCard : Card
{
    protected List<QueuePerson> removeThese;

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
        RemoveEvenPeople();
    }

    protected void RemoveEvenPeople()
    {
        removeThese = new List<QueuePerson>();
        // get all people in even spots
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (i % 2 == 1)
                {
                    removeThese.Add(GameManager.instance.queueManager.currentQueue[i]);
                }
            }
        }
        // remove them from queue

        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
