using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBtn : MonoBehaviour
{
    public GameObject canvas_Menu;
    public Text txt_Setting;
    public Text txt_ToMain;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuClose();
        }
    }

    public void menuOpen()
    {
        canvas_Menu.SetActive(true);
    }

    public void menuClose()
    {
        canvas_Menu.SetActive(false);
    }

    public void toMain()
    {
        SceneManager.LoadScene("Title");
    }

    public void btn_SettingHover()
    {
        txt_Setting.color = Color.black;
    }
    public void btn_SettingDefault()
    {
        txt_Setting.color = Color.white;
    }
    public void btn_ToMainHover()
    {
        txt_ToMain.color = Color.black;
    }
    public void btn_ToMainDefault()
    {
        txt_ToMain.color = Color.white;
    }
}
