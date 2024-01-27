using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveTrapdoorCard : Card
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
            RemoveTrapdoors();
        }
    }

    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void RemoveTrapdoors()
    {
        // get all people on trapdoors

        // remove them

        // add them to end of queue in same order
    }
}
