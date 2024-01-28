using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewCutscene", menuName = "Data/Cutscene", order = 3)]
public class Cutscene : ScriptableObject
{
    public List<CutsceneScreen> screens;
    public LevelData levelToLoadWhenFinished;
}
