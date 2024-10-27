using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviour
{
    public GameObject pauseFrame;
    public PauseFrame pause;
    public GameObject noticeFrame;
    public NoticeFrame notice;
    public GameObject settingFrame;
    public SettingFrame setting;

    public Animator popupBack;

    bool isMenuOpen = false;
    bool isToMainOpen = false; 
    bool isExitOpen = false; 
    bool isSettingOpen = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMenuOpen && !isToMainOpen && !isExitOpen && !isSettingOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pauseFrame.SetActive(true);
                PlayerController.ishit = true;
                Time.timeScale = 0;
                isMenuOpen = true;
                pause.pause_Open();
            }
        }
        else if (isMenuOpen)
        {
            pause.menuUpdate();

            menuSelect();

            if (Input.GetKeyDown(KeyCode.E))
            {
                pause.pause_Close();
                popupBack.Play("popupBack_close");
                isMenuOpen = false;

                PlayerController.ishit = false;
                Time.timeScale = 1;

            }
        }

        if (isToMainOpen && !isMenuOpen && notice.isendAni == true)
        {
            notice.noticeUpdate();
            toMainSelect();

        }

        if (isExitOpen && !isMenuOpen && notice.isendAni == true)
        {
            notice.noticeUpdate();
            exitSelect();

        }

        if (isSettingOpen && !isMenuOpen)
        {
            setting.settingUpdate();

            setting.settingControl();

            if (Input.GetKeyDown(KeyCode.E))
            {
                setting.setting_Close();
                isSettingOpen = false;

                PlayerController.ishit = false;
                Time.timeScale = 1;
            }
        }
    }


    public void menuSelect()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pause.menuNum == 0)
            {
                isMenuOpen = false;

                pause.pause_Close();
                settingFrame.SetActive(true);
                setting.setting_Open();

                isSettingOpen = true;
                //설정창 오픈
            }
            else if (pause.menuNum == 1)
            {
                isMenuOpen = false;

                pause.pause_Close();
                noticeFrame.SetActive(true);
                notice.setText_toMain(); //노티스 텍스트 설정
                notice.notice_Open(); //노티스 창 오픈

                isToMainOpen = true;

            }
            else if (pause.menuNum == 2)
            {
                isMenuOpen = false;

                pause.pause_Close();
                noticeFrame.SetActive(true);
                notice.setText_Exit();
                notice.notice_Open();

                isExitOpen = true;

            }
        }

    }

    public void toMainSelect()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (notice.isyes == true)
            {
                //메인화면으로 돌아감
                SceneManager.LoadScene("Main");
            }
            else if (notice.isyes == false)
            {
                isToMainOpen = false;
                notice.notice_Close();

                PlayerController.ishit = false;
                Time.timeScale = 1;
            }
        }

    }

    public void exitSelect()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (notice.isyes == true)
            {
                //게임 종료
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();

#endif
            }
            else if (notice.isyes == false)
            {
                isExitOpen = false;
                notice.notice_Close();

                PlayerController.ishit = false;
                Time.timeScale = 1;
            }
        }

    }


    //esc를 누르면 시간 정지, 메뉴 오픈
}
