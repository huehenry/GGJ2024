using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBouncer : MonoBehaviour
{
    private float lerper;
    private float pingPong;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lerper += Time.deltaTime;
        pingPong = 0.75f+Mathf.PingPong(lerper*0.75f, 0.45f);
        this.gameObject.transform.localScale = new Vector3(pingPong, pingPong, pingPong);
        Vector3 rot = this.gameObject.transform.localEulerAngles;
        rot.y += Time.deltaTime* 180;
        this.gameObject.transform.localRotation = Quaternion.Euler(rot);
    }
}
