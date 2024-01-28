using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNotJeansCard : Card
{
    public List<QueuePerson> removeThese;
    // Start is called before the first frame update
    public override void CallEffect()
    {
        RemoveNotJeans();
    }

    protected void RemoveNotJeans()
    {
        removeThese = new List<QueuePerson>();
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
