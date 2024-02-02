using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _audioManager;
    
    public AudioClip[] beep;
    public AudioClip[] cardShuffle;
    public AudioClip[] playCard;
    public AudioClip[] fizzleCard;
    public AudioClip[] victory;
    public AudioClip[] wilheilm;
    public List<AudioClip> grumbles;

    private void Awake()
    {
        if (_audioManager == null)
        {
            _audioManager = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
 
    public void PlaySound( AudioClip[] clip )
    {
        AudioSource.PlayClipAtPoint(clip[Random.Range(0,clip.Length)], Camera.main.transform.position);
    }

    public void PlayRandomGrumble()
    {
        if (grumbles.Count > 0)
        {
            AudioSource.PlayClipAtPoint(grumbles[Random.Range(0,grumbles.Count)], Camera.main.transform.position);
        }
    }

}
