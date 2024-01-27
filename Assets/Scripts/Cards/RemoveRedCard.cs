using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRedCard : Card
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
            RemoveReds();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void RemoveReds()
    {
        // get all red shirt people

        // remove them from queue

        // add them to end of queue in same order
    }
}
