using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPerson", menuName = "Data/QueuePersonData", order = 2)]
[System.Serializable]
public class QueuePersonData : ScriptableObject
{
    public enum HeightType { TALL, NORM, SHORT };

    [HideInInspector] public float UID;
    public bool isPlayer = false;
    public HeightType height = HeightType.TALL;
    public Material shirtMaterial;
    public Material pantMaterial;

    public void Start()
    {
        // Give them a random ID
        UID = DateTime.Now.Ticks;
    }
}
