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
    public LevelData currentLevel;

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
        currentLevel = levelData;
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
        UI_CardInventory._cardInventory.levelText.text = levelData.levelName;

        // Now, make all the people
        //JEREMY: Gonna do this with a list
        List<QueuePerson> personList = new List<QueuePerson>();



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
                                                                new Vector3(-20,0.5f,0),
                                                                Quaternion.Euler(0,95,0));

                // Set the data for that person
                QueuePerson queuePerson = tempPerson.GetComponent<QueuePerson>();
                queuePerson.LoadFromScriptableObject(levelData.queue[i]);

                //ADDING THEM TO OUR LIST
                personList.Add(queuePerson);

                // TODO: Create a number for them on the screen -- This can be hard in the gameplay scene.

                // TODO: Arrow that shows this is "US" over the player!

            }
            GameManager.instance.queueManager.LoadNewQueue(personList, levelData.trapdoors);
            //Set lastlevel
            if (levelData.finalLevel == true)
            {
                GameManager.instance.queueManager.lastLevel = true;
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
            // Debug.Log("Loading Gameplay Scene: " + gameplaySceneName);
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
            // Debug.Log("Unloading old background: " + backgroundSceneName);
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
