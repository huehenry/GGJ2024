using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [Header("GameData")]
    public QueueManager queueManager;
    public Transform offscreenSpawnPoint;
    public bool isPaused = false;

    [Header("Prefabs")]
    public GameObject pfUI;
    public GameObject pfPerson;

    [Header("Sounds")]
    public AudioClip scream;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

}
