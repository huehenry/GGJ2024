using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager _cutsceneManager;
    public Image image;
   

    void Awake()
    {
        if (_cutsceneManager == null)
        {
            _cutsceneManager = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

     public void PlayCutsceneThenLoadLevel(Cutscene cutscene)
    {
        // Play the Cutscene
        StartCoroutine(DoPlayCutscene(cutscene));
    }

    private IEnumerator DoPlayCutscene(Cutscene cutscene)
    {
        // Pause the game
        GameManager.instance.isPaused = true;

        // Play the cutscene
        image.enabled = true;

        foreach (CutsceneScreen screen in cutscene.screens)
        {
            Debug.Log("showing screen " + screen.image.name);
            // Show the image
            if (screen.image != null)
            {
                image.sprite = screen.image;
            } else
            {
                image.sprite = null;
            }

            // Play the sound
            if (screen.soundClip != null)
            {
                AudioSource.PlayClipAtPoint(screen.soundClip, Camera.main.transform.position);
            }

            // Wait the delay
            Debug.Log("Waiting " + screen.image.name + screen.timeOnScreen);
            yield return new WaitForSeconds(screen.timeOnScreen);
            Debug.Log("Done Waiting " + screen.image.name + screen.timeOnScreen);
        }

        // Hide the cutscene
        image.enabled = false;

        // Load the level
        GameLoader.instance.LoadLevel(cutscene.levelToLoadWhenFinished);

        // Unpause the game
        GameManager.instance.isPaused = false;

        yield break;
    }


}
