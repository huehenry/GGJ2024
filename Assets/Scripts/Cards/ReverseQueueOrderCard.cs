using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseQueueOrderCard : Card
{
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
    }
    protected void ReverseQueue()
    {
        // remove all people in queue

        // replace them in opposite order
    }
}
