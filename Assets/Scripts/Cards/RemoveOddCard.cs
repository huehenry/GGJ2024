using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOddCard : Card
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
        if (isUsed != true)
        {
            isUsed = true;
            RemoveOddPeople();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
        removeThese.Clear();
    }

    protected void RemoveOddPeople()
    {
        // get all people in odd spots
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (i % 2 == 0)
                {
                    removeThese.Add(GameManager.instance.queueManager.currentQueue[i]);
                }
            }
        }
        // remove them from queue 

        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
