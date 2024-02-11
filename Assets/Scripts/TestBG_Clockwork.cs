using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBG_Clockwork : MonoBehaviour
{
    public GameObject[] allPeople;
    private Vector3[] allLoc;
    private Quaternion[] allRot;

    public float interval = 1.0715f;
    private float ticker;
    private bool on;

    // Start is called before the first frame update
    void Start()
    {
        allLoc = new Vector3[allPeople.Length];
        allRot = new Quaternion[allPeople.Length];
        for(int i = 0; i < allPeople.Length; i++)
		{
            allLoc[i] = allPeople[i].transform.localPosition;
            allRot[i] = allPeople[i].transform.localRotation;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            ticker += Time.deltaTime;
            if (ticker < interval)
            {
                float smooth = Mathf.Lerp(0, 1, ticker / interval);
                for (int i = 0; i < allPeople.Length - 1; i++)
                {
                    allPeople[i].transform.localPosition = Vector3.Lerp(allLoc[i], allLoc[i + 1], smooth);
                    allPeople[i].transform.localRotation = Quaternion.Lerp(allRot[i], allRot[i + 1], smooth);
                }
                allPeople[allPeople.Length - 1].transform.localPosition = allLoc[0];
                allPeople[allPeople.Length - 1].transform.localRotation = allRot[0];
            }
			else
			{
                ticker = 0;
                on = false;
                //Cycle the people.
                GameObject[] temp = new GameObject[allPeople.Length];
                temp[0] = allPeople[allPeople.Length - 1];
                for(int i = 1; i < allPeople.Length;i++)
				{
                    temp[i] = allPeople[i - 1];
				}
                allPeople = new GameObject[temp.Length];
                for(int i = 0; i < allPeople.Length; i++)
				{
                    allPeople[i] = temp[i];
				}
			}
		}
		else
		{
            ticker += Time.deltaTime;
            if(ticker>= interval)
			{
                ticker = 0;
                on = true;
			}
		}
    }
}
