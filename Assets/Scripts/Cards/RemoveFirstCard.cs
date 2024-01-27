using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFirstCard : Card
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
            RemoveFirstPerson();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void RemoveFirstPerson()
    {
        // find first person in line

        // remove them from queue

        // add them to end of queue
    }
}
