using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBlueCard : Card
{
    public List<QueuePerson> removeThese;
    // Start is called before the first frame update
    public override void CallEffect()
    {
        if (isUsed != true)
        {
            isUsed = true;
            RemoveBlues();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
        removeThese.Clear();
    }
    protected void RemoveBlues()
    {
        // get all blue shirt people
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.currentQueue[i].shirt == ShirtType.BLUE)
                {
                    removeThese.Add(GameManager.instance.queueManager.currentQueue[i]);
                }
            }
        }
        // remove them from queue
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
