using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNotJeansCard : Card
{
    public List<QueuePerson> removeThese;
    // Start is called before the first frame update
    public override void CallEffect()
    {
        if (isUsed != true)
        {
            isUsed = true;
            RemoveNotJeans();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
        removeThese.Clear();
    }
    protected void RemoveNotJeans()
    {
        // get all not jeans
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.currentQueue[i].pants != PantsType.JEANS)
                {
                    removeThese.Add(GameManager.instance.queueManager.currentQueue[i]);
                }
            }
        }
        // remove them from queue
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
