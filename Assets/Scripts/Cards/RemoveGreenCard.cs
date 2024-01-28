using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGreenCard : Card
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
            RemoveGreens();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
        removeThese.Clear();
    }
    protected void RemoveGreens()
    {
        // get all green shirt people
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.currentQueue[i].shirt == ShirtType.GREEN)
                {
                    removeThese.Add(GameManager.instance.queueManager.currentQueue[i]);
                }
            }
        }
        // remove them from queue
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
