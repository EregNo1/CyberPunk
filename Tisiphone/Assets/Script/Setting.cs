using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public GameObject canvas_menu;
    public GameObject canvas_Setting;


    public Button btn_BGM_On;
    public Button btn_BGM_Off;
    public Button btn_SFX_On;
    public Button btn_SFX_Off;
    public Button btn_Mode_full;
    public Button btn_Mode_window;


    public Sprite img_Sound_On_select;
    public Sprite img_Sound_On_default;
    public Sprite img_Sound_Off_select;
    public Sprite img_Sound_Off_default;
    public Sprite img_Mode_full_select;
    public Sprite img_Mode_full_default;
    public Sprite img_Mode_window_select;
    public Sprite img_Mode_window_default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas_Setting.SetActive(false);
        }
    }

    public void settingOpen()
    {
        canvas_Setting.SetActive(true);
    }
    public void settingClose()
    {
        canvas_Setting.SetActive(false);
        canvas_menu.SetActive(true);
    }



    public void bgmSoundOn()
    {
 //       AudioManager.instance.bgMusicVolume(1f);

        btn_BGM_On.image.sprite = img_Sound_On_select;
        btn_BGM_Off.image.sprite = img_Sound_Off_default;
    }

    public void bgmSoundOff()
    {
//        AudioManager.instance.bgMusicVolume(0f);

        btn_BGM_On.image.sprite = img_Sound_On_default;
        btn_BGM_Off.image.sprite = img_Sound_Off_select;
    }

    public void sfxSoundOn()
    {
 //       AudioManager.instance.sfxMusicVolume(1f);

        btn_SFX_On.image.sprite = img_Sound_On_select;
        btn_SFX_Off.image.sprite = img_Sound_Off_default;
    }

    public void sfxSoundOff()
    {
 //       AudioManager.instance.sfxMusicVolume(0f);

        btn_SFX_On.image.sprite = img_Sound_On_default;
        btn_SFX_Off.image.sprite = img_Sound_Off_select;
    }

    public void modeFull()
    {
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }

        //버튼 형태 변경
        btn_Mode_full.image.sprite = img_Mode_full_select;
        btn_Mode_window.image.sprite = img_Mode_window_default;
    }

    public void modeWindow()
    {
        if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        //버튼 형태 변경
        btn_Mode_full.image.sprite = img_Mode_full_default;
        btn_Mode_window.image.sprite = img_Mode_window_select ;
    }
}
