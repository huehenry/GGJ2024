using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewPerson", menuName = "Data/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public List<QueuePersonData> queue;
    public string backgroundSceneFilename;
}