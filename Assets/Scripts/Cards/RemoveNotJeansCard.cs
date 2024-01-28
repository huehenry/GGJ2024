using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveNotJeansCard : Card
{
    // Start is called before the first frame update
    public override void CallEffect()
    {
        if (isUsed != true)
        {
            isUsed = true;
            RemoveNotJeans();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void RemoveNotJeans()
    {
        
    }
}
