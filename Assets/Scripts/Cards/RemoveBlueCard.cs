using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBlueCard : Card
{
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
    }
    protected void RemoveBlues()
    {
        // get all blue shirt people

        // remove them from queue

        // add them to end of queue in same order
    }
}
