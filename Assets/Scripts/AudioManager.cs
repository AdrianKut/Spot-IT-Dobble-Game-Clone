using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioClipType
{
    wrongIcon = 0,
    correctIcon = 1,
    gameOver = 2,

}


public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] audioClips;

    private AudioSource audioSource;

    private AudioManager() { }
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayOnceClip(AudioClipType audioClipType)
    {
        audioSource.PlayOneShot(audioClips[(int)audioClipType]);
    
    }
    
}
