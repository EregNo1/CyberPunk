using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgMusic;
    public AudioSource sfxMusic;


    [Header("BGM�����Ŭ��")]
    public AudioClip bgm_Main;

    [Header("SFX�����Ŭ��")]
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



    /*ȿ���� ��� ����
    public void Play_itemGet()
    {
        sfx.PlayOneShot(item_Get);
    }*/
}
