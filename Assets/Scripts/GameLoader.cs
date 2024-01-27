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

 
    public void LoadLevel(LevelData levelData)
    {
        // Load the level and gameplay scene
        StartCoroutine(DoLoadLevel(levelData));
    }



    public void LoadLevel (string backgroundToLoad)
    {
        StartCoroutine(DoLoadLevelScenes(backgroundToLoad));
    }


    public IEnumerator DoLoadLevel(LevelData levelData)
    {
        // Load the scenes
        yield return DoLoadLevelScenes(levelData.backgroundSceneFilename);

        // After that is done, load the cards into the UI
        if (UI_CardInventory._cardInventory == null)
        {
            Debug.LogError("ERROR: CARD UI NOT LOADED IN GAMEPLAY SCENE");
            yield break;
        }
        UI_CardInventory._cardInventory.NewLevel(levelData.CardList.ToArray());

        // Now, make all the people
        if (GameManager.instance == null)
        {
            Debug.LogError("ERROR: NO GAME MANAGER LOADED");
        }
        else
        {
            // Instantiate the person
            for (int i = 0; i < levelData.queue.Count; i++)
            {
                GameObject tempPerson = Instantiate<GameObject>(GameManager.instance.pfPerson,
                                                                GameManager.instance.PeopleSpawnPoints[i].position,
                                                                GameManager.instance.PeopleSpawnPoints[i].rotation);

                // Set the data for that person
                QueuePerson queuePerson = tempPerson.GetComponent<QueuePerson>();
                queuePerson.LoadFromScriptableObject(levelData.queue[i]);

                // Save them in the game manager
                GameManager.instance.persons.Add(queuePerson);

                // TODO: Create a number for them on the screen

            }
        }
        yield return null;
    }



    public IEnumerator DoLoadLevelScenes(string backgroundToLoad)
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
