using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOddCard : Card
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
            RemoveOddPeople();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }

    protected void RemoveOddPeople()
    {
        // get all people in odd spots

        // remove them from queue

        // add them to end of queue in the same order
    }
}
