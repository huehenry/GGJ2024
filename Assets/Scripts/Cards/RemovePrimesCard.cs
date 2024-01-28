using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePrimesCard : Card
{
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
        RemovePrimePeople();
    }

    protected void RemovePrimePeople()
    {
        removeThese = new List<QueuePerson>();
        // get all people in prime spots
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {

                if (i == 1 || i == 3 || i == 5 || i==7)
                {
                    removeThese.Add(GameManager.instance.queueManager.currentQueue[i]);
                }
            }
        }
        // remove them from queue

        GameManager.instance.queueManager.Deletion(removeThese);
    }
}
