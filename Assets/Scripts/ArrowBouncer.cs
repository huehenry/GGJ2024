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
        pingPong = 3.75f+Mathf.PingPong(lerper*1.5f, 0.65f);
        this.gameObject.transform.localPosition = new Vector3(0, pingPong, 0);
        Vector3 rot = this.gameObject.transform.localEulerAngles;
        rot.y += Time.deltaTime* 180;
        this.gameObject.transform.localRotation = Quaternion.Euler(rot);
    }
}
