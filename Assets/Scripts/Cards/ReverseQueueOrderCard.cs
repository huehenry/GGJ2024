using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseQueueOrderCard : Card
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
            ReverseQueue();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
        removeThese.Clear();
    }
    protected void ReverseQueue()
    {
        // remove all people in queue
        for (int i = GameManager.instance.queueManager.currentQueue.Count; i > 0; i--)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                removeThese.Add(GameManager.instance.queueManager.currentQueue[i]);
            }
        }
        // replace them in opposite order
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
