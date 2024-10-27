using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgMusic;
    public AudioSource sfxMusic;


    [Header("BGM오디오클립")]
    public AudioClip bgm_Main;

    [Header("SFX오디오클립")]
    public AudioClip item_Get;
    public AudioClip ddd;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void bgMusicVolume(float bgVolume)
    {
        bgMusic.volume = bgVolume;
    }

    public void sfxMusicVolume(float sfxVolume)
    {
        bgMusic.volume = sfxVolume;
    }



    /*효과음 재생 예시
    public void Play_itemGet()
    {
        sfx.PlayOneShot(item_Get);
    }*/
}
