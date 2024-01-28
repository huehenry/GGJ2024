using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveFirstCard : Card
{
    protected QueuePerson firstPerson;
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
        if (isUsed != true)
        {
            isUsed = true;
            RemoveFirstPerson();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
        removeThese.Clear();
    }
    protected void RemoveFirstPerson()
    {
        // find first person in line
        firstPerson = GameManager.instance.queueManager.currentQueue[0];
        // remove them from queue
        removeThese.Add(firstPerson);
        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
