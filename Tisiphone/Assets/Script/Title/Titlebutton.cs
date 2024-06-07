using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titlebutton : MonoBehaviour
{
    public Animator dial1;
    public Animator dial2;

    // Start is called before the first frame update
    void Start()
    {

        dial2 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void gameExitHover()
    {
        dial2.SetBool("ishover2", true);
    }
    public void gameExitCancel()
    {
        dial2.SetBool("ishover2", false);
    }
    //게임 시작 함수
    public void gameStart()
    {
        SceneManager.LoadScene("Game");
    }

    //게임 종료 함수
    public void gameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();

#endif
    }
}
