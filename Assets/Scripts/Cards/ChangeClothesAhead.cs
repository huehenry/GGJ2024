using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothesAhead : Card
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
        SwapClothesAhead();
    }

    protected void SwapClothesAhead()
    {
        // get player position
        List<QueuePerson> spinMe = new List<QueuePerson>();
        for (int i = 0; i < GameManager.instance.queueManager.currentQueue.Count; i++)
        {
            if (GameManager.instance.queueManager.currentQueue[i] != null)
            {
                if (GameManager.instance.queueManager.currentQueue[i].isPlayer == true)
                {
                    if(i > 0)
					{
                        ShirtType s = GameManager.instance.queueManager.currentQueue[i].shirt;
                        GameManager.instance.queueManager.currentQueue[i].shirt = GameManager.instance.queueManager.currentQueue[i - 1].shirt;
                        GameManager.instance.queueManager.currentQueue[i - 1].shirt = s;
                        PantsType p = GameManager.instance.queueManager.currentQueue[i].pants;
                        GameManager.instance.queueManager.currentQueue[i].pants = GameManager.instance.queueManager.currentQueue[i - 1].pants;
                        GameManager.instance.queueManager.currentQueue[i - 1].pants = p;
                        spinMe.Add(GameManager.instance.queueManager.currentQueue[i]);
                        spinMe.Add(GameManager.instance.queueManager.currentQueue[i-1]);
                    }
                    break;
                    
                }
            }
        }
        if (spinMe.Count > 0)
        {
            GameManager.instance.queueManager.UpdateVisuals(spinMe);
		}
		else
		{
            GameManager.instance.queueManager.Fizzle();

        }
    }
}
