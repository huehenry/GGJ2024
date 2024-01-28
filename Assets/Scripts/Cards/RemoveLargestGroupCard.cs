using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLargestGroupCard : Card
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
            RemoveLargestGroup();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }

    protected void RemoveLargestGroup()
    {
        //TODO: Remove largest group
    }
}
