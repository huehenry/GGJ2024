using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Data/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public List<QueuePersonData> queue;
    public string levelName;
    public List<Card> CardList;
    public string backgroundSceneFilename;
    public List<bool> trapdoors;
    public Cutscene nextCutscene;
    public bool finalLevel = false;
}