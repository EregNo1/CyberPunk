using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingFrame : MonoBehaviour
{
    public Animator animator;
    public Animator popupBack;

    public int settingNum = 0;
    public bool isendAni = false;

    public Sprite arrowL_On;
    public Sprite arrowL_Off;
    public Sprite arrowR_On;
    public Sprite arrowR_Off;

    [Header("BGM ���� ���")]
    public RectTransform bgm_Gage;
    public Text txt_bgmNum;
    int bgmGage_W;
    float bgmVolume;

    [Header("SFX ���� ���")]
    public RectTransform sfx_Gage;
    public Text txt_sfxNum;
    int sfxGage_W;
    float sfxVolume;

    [Header("ȭ���� ���� ���")]
    public Text txt_Mode;
    public Image mode_arrow_L;
    public Image mode_arrow_R;

    [Header("Ű ���� ���� ���")]
    public Text txt_KeySwap;
    public Image key_arrow_L;
    public Image key_arrow_R;


    int bgmNum = 10; //����� ��ġ
    int sfxNum = 10; //ȿ���� ��ġ
    bool screenMode; // ȭ����
    bool keySwap; //Ű ����


    public void setting_Open()
    {
        settingNum = 0;
        animator.Play("setting_ap");
    }
    public void setting_Close()
    {
        animator.SetTrigger("isClose");
        popupBack.Play("popupBack_close");
        isendAni = false;
    }
    public void setFalse()
    {
        gameObject.SetActive(false);
    }

    public void isEndAni()
    {
        isendAni = true;
    }

    public void settingUpdate()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (settingNum < 3)
            {
                settingNum++;
                animator.SetInteger("settingNum", settingNum);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (settingNum > 0)
            {
                settingNum--;
                animator.SetInteger("settingNum", settingNum);
            }
        }

    }

    public void settingControl()
    {
        if (settingNum == 0) //����� ����
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (bgmNum > 0)
                {
                    bgmNum--;
                    bgmGage_W = 19 * bgmNum + 4;
                    bgmVolume = bgmNum / 10;

                    bgm_Gage.sizeDelta = new Vector2(bgmGage_W, 11);
                    txt_bgmNum.text = bgmNum.ToString();

                    AudioManager.instance.bgMusicVolume(bgmVolume);
                }

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (bgmNum < 10)
                {
                    bgmNum++;
                    bgmGage_W = 19 * bgmNum;
                    bgmVolume = bgmNum / 10;

                    bgm_Gage.sizeDelta = new Vector2(bgmGage_W, 11);
                    txt_bgmNum.text = bgmNum.ToString();

                    AudioManager.instance.bgMusicVolume(bgmVolume);
                }

            }
        }
        else if (settingNum == 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(sfxNum > 0)
                {
                    sfxNum--;
                    sfxGage_W = 19 * sfxNum + 4;
                    sfxVolume = sfxNum / 10;

                    sfx_Gage.sizeDelta = new Vector2(sfxGage_W, 11);
                    txt_sfxNum.text = sfxNum.ToString();
                    AudioManager.instance.sfxMusicVolume(sfxVolume);
                }

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(sfxNum < 10)
                {
                    sfxNum++;
                    sfxGage_W = 19 * sfxNum + 4;
                    sfxVolume = sfxNum / 10;

                    sfx_Gage.sizeDelta = new Vector2(sfxGage_W, 11);
                    txt_sfxNum.text = sfxNum.ToString();
                    AudioManager.instance.sfxMusicVolume(sfxVolume);
                }
            }
            //ȿ���� ����
        }
        else if (settingNum == 2) //ȭ����
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) //��üȭ��
            {
                txt_Mode.text = "��üȭ��";
                mode_arrow_L.sprite = arrowL_Off;
                mode_arrow_R.sprite = arrowR_On;

                if (Screen.fullScreenMode == FullScreenMode.Windowed)
                {
                    Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) //â���
            {
                txt_Mode.text = "â���";
                mode_arrow_L.sprite = arrowL_On;
                mode_arrow_R.sprite = arrowR_Off;

                if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
                {
                    Screen.fullScreenMode = FullScreenMode.Windowed;
                }
            }
        }
        else if (settingNum == 3) //Ű ����
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) //OFF
            {
                txt_KeySwap.text = "OFF";
                key_arrow_L.sprite = arrowL_Off;
                key_arrow_R.sprite = arrowR_On;

                PlayerController.keySwap = false;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) //ON
            {
                txt_KeySwap.text = "ON";
                key_arrow_L.sprite = arrowL_On;
                key_arrow_R.sprite = arrowR_Off;

                PlayerController.keySwap = true;
            }
        }
    }

}
