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
        removeThese = GameManager.instance.queueManager.CandyCrushMe();
        // remove them from queue
        if(removeThese.Count>0)
		{
            GameManager.instance.queueManager.Deletion(removeThese);

        }
        else
		{
            GameManager.instance.queueManager.Fizzle();

        }
    }
}
