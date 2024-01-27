using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEvenCard : Card
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
            RemoveEvenPeople();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }

    protected void RemoveEvenPeople()
    {
        // get all people in even spots

        // remove them from queue

        // add them to end of queue in the same order
    }
}
