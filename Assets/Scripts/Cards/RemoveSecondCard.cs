using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSecondCard : Card
{
    protected QueuePerson secondPerson;
    protected List<QueuePerson> removeThese;
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
        RemoveSecondPerson();
    }

    protected void RemoveSecondPerson()
    {
        removeThese = new List<QueuePerson>();
        // find second person in line
        secondPerson = GameManager.instance.queueManager.currentQueue[1];
        // remove them from queue
        removeThese.Add(secondPerson);
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
