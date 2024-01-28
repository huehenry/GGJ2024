using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTheLine : Card
{
    // Start is called before the first frame update
    public override void CallEffect()
    {
        if (isUsed != true)
        {
            isUsed = true;
            DoAction();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void DoAction()
    {
        // Say "Next Please"
        // Check for Win Condition
    }
}
