using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainControl : MonoBehaviour
{

    public Image txt_NewGame;
    public Image txt_LoadGame;
    public Image txt_Config;
    public Image txt_Quit;


    public Sprite newGame_off;
    public Sprite newGame_on;
    public Sprite loadGame_off;
    public Sprite loadGame_on;
    public Sprite config_off;
    public Sprite config_on;
    public Sprite quit_off;
    public Sprite quit_on;


    public GameObject window_Config;
    public Animator titleMenu;

    bool isVideoEnd = false;

    int menucode = 0;
    public static bool isConfigOpen = false;

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
    }


    private void Start()
    {
        StartCoroutine(titleMenuOn());
    }

    // Update is called once per frame
    void Update()
    {
        if (isVideoEnd)
        {
            if (isConfigOpen == false)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (menucode < 3)
                    {
                        menucode++;
                    }

                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (menucode > 0)
                    {
                        menucode--;
                    }

                }


                if (menucode == 0)
                {
                    txt_NewGame.sprite = newGame_on;
                    txt_LoadGame.sprite = loadGame_off;
                    txt_Config.sprite = config_off;
                    txt_Quit.sprite = quit_off;

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        SceneManager.LoadScene("Game"); //���ӽ��� (Game Scene �ҷ�����)
                                                        //����� ������ ���� ���: ������ ���� ��� ���


                    }

                }

                if (menucode == 1)
                {
                    txt_NewGame.sprite = newGame_off;
                    txt_LoadGame.sprite = loadGame_on;
                    txt_Config.sprite = config_off;
                    txt_Quit.sprite = quit_off;

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        //����� ���ӽ��� (Game Scene �ҷ�����)
                    }

                }

                if (menucode == 2)
                {
                    txt_NewGame.sprite = newGame_off;
                    txt_LoadGame.sprite = loadGame_off;
                    txt_Config.sprite = config_on;
                    txt_Quit.sprite = quit_off;

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        window_Config.SetActive(true); //Config Window ����
                        isConfigOpen = true;
                    }

                }

                if (menucode == 3)
                {
                    txt_NewGame.sprite = newGame_off;
                    txt_LoadGame.sprite = loadGame_off;
                    txt_Config.sprite = config_off;
                    txt_Quit.sprite = quit_on;

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameExit(); //���� ����. ����Ƽ �����Ϳ����� ���� �ùķ��̼� ����
                    }

                }
            }
        }
       


      



        if (isConfigOpen == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                window_Config.SetActive (false);
                isConfigOpen = false;
            }
        }
    }


    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();

#endif

    }


    IEnumerator titleMenuOn()
    {
        yield return new WaitForSeconds(15f);
        titleMenu.Play("titleMenu_On");

        yield return new WaitForSeconds(2f);

        isVideoEnd = true;


    }

}
