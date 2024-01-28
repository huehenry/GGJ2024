using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPerson", menuName = "Data/QueuePersonData", order = 2)]
[System.Serializable]
public class QueuePersonData : ScriptableObject
{
    public bool isPlayer = false;
    public ShirtType shirt;
    public PantsType pants;
    public HeightType height = HeightType.TALL;
    // TODO: Variations? 

}

public enum HeightType { TALL, NORM, SMALL };
public enum ShirtType { RED, GREEN, BLUE };
public enum PantsType { JEANS, SHORTS, LEATHER, NONE };

