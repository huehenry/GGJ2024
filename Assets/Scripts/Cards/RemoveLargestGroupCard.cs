using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLargestGroupCard : Card
{
    public List<QueuePerson> removeThese;

    public List<QueuePerson> largestReds;
    public List<QueuePerson> largestGreens;
    public List<QueuePerson> largestBlues;
    public List<QueuePerson> largestJeans;
    public List<QueuePerson> largestShorts;
    public List<QueuePerson> largestLeathers;
    public List<QueuePerson> largestNudes; // if this is still relevant
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
            RemoveLargestGroup();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
        removeThese.Clear();
    }

    protected void RemoveLargestGroup()
    {
        //TODO: Remove largest group

        // find largest group of red shirts
        // find largest group of green shirts
        // find largest group of blue shirts
        // find largest group of jeans
        // find largest group of shorts
        // find largest group of leather pants
        // find largest group of no pants if that's still relevant

        // identify which of these groups is The Largest
        // add them to removeThese

        // remove them from queue
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
