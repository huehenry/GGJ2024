using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSetsOfThreeCard : Card
{
    public List<QueuePerson> removeThese;
    // Start is called before the first frame update
    public override void CallEffect()
    {
        RemoveTriples();
    }
    protected void RemoveTriples()
    {
        removeThese = new List<QueuePerson>();
        // get all sets of 3 consecutive same shirt colors
        // add them to removeThese

        // remove them from queue
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
