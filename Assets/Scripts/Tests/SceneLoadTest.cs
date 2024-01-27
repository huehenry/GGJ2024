using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneLoadTest : MonoBehaviour
{
    public TMP_InputField input;
    public void LoadTestLevel ()
    {
        Debug.Log("Loading " + input.text);
        GameLoader.instance.LoadLevel(input.text);
    }
}
