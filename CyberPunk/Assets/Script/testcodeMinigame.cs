using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testcodeMinigame : MonoBehaviour
{
    public GameObject canvas_minigamebutton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonOn()
    {
        canvas_minigamebutton.SetActive(true);
    }

    public void pinpongStart()
    {
        SceneManager.LoadScene("PingPong");
    }

    public void typingStart()
    {
        SceneManager.LoadScene("Typing");
    }
}
