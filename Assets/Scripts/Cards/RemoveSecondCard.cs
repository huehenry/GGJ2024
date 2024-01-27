using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSecondCard : Card
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
            RemoveSecondPerson();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void RemoveSecondPerson()
    {
        // find second person in line

        // remove them from queue

        // add them to end of queue
    }
}
