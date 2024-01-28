using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSetsOfThreeCard : Card
{
    public List<QueuePerson> removeThese;
    // Start is called before the first frame update
    public override void CallEffect()
    {
        if (isUsed != true)
        {
            isUsed = true;
            RemoveTriples();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
        removeThese.Clear();
    }
    protected void RemoveTriples()
    {
        // get all sets of 3 consecutive same shirt colors
        // add them to removeThese

        // remove them from queue
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
