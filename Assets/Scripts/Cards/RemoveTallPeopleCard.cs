using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTallPeopleCard : Card
{
    public List<QueuePerson> removeThese;
    // Start is called before the first frame update
    public override void CallEffect()
    {
        RemoveTalls();

    }

    protected void RemoveTalls()
    {
        removeThese = new List<QueuePerson>();
        // get all tall people
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.currentQueue[i].height == HeightType.TALL)
                {
                    removeThese.Add(GameManager.instance.queueManager.currentQueue[i]);
                }
            }
        }
        // remove them from queue
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
