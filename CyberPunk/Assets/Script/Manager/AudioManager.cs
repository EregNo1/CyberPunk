using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource bgMusic;
    public AudioSource sfxMusic;


    public static AudioManager instance;


    public void Awake()
    {
        instance = this;   
        //DontDestroyOnLoad(this.gameObject);
    }

    public void bgMusicVolume(float bgVolume)
    {
        bgMusic.volume = bgVolume;
    }

    public void sfxMusicVolume(float sfxVolume)
    {
        bgMusic.volume = sfxVolume;
    }

}
