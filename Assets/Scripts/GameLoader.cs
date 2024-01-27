using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class GameLoader : MonoBehaviour
{
    public static GameLoader instance;
    private string gameplaySceneName = "Gameplay";
    private string backgroundSceneName;

    private void Awake()
    {
        if (instance == null) { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void LoadLevel (string backgroundToLoad)
    {
        StartCoroutine(DoLoadLevel(backgroundToLoad));
    }

    public IEnumerator DoLoadLevel(string backgroundToLoad)
    {
        AsyncOperation asyncLoad;

        // If gameplay is not loaded, load the gameplay
        Scene gameplayScene = SceneManager.GetSceneByName(gameplaySceneName);
        if (!gameplayScene.IsValid())
        {
            Debug.Log("Loading Gameplay Scene: " + gameplaySceneName);
            asyncLoad = SceneManager.LoadSceneAsync(gameplaySceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        // If there is already a background, remove it.
        Scene backgroundScene = SceneManager.GetSceneByName(backgroundSceneName);
        if (backgroundScene.IsValid())
        {
            Debug.Log("Unloading old background: " + backgroundSceneName);
            asyncLoad = SceneManager.UnloadSceneAsync(backgroundSceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        // Load the background
        Debug.Log("Loading New background: " + backgroundToLoad);
        asyncLoad = SceneManager.LoadSceneAsync(backgroundToLoad, LoadSceneMode.Additive);
        backgroundSceneName = backgroundToLoad;

        // Wait ("next frame") for the load to finish
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
