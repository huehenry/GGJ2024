using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonLoadTest : MonoBehaviour
{
    public QueuePersonData iDataToLoad;
    public QueuePersonData oDataToLoad;
    public QueuePersonData pDataToLoad;
    public QueuePerson personToChange;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            personToChange.LoadFromScriptableObject(pDataToLoad);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            personToChange.LoadFromScriptableObject(oDataToLoad);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            personToChange.LoadFromScriptableObject(iDataToLoad);
        }

    }
}
