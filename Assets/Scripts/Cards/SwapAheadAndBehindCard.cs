using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapAheadAndBehindCard : Card
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
            SwapAheadAndBehind();
        }
    }
    public override void RefreshCard()
    {
        isUsed = false;
    }
    protected void SwapAheadAndBehind()
    {
        // get player's position

        // get ahead person's position

        // get behind person's position

        // remove them both from queue

        // place ahead at behind's old position

        // place behind at ahead's old position
    }
}
