using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGreenCard : Card
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
            RemoveGreens();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void RemoveGreens()
    {
        // get all green shirt people

        // remove them from queue

        // add them to queue in same order
    }
}
