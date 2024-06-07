using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typing : MonoBehaviour
{
    //time text
    //Question text
    //Input field

    public Text txt_Timer;
    public Text txt_Q;
    public InputField inputfield;
    public Text txt_A;

    string[] wordArray_Q =
    {
        "Game", "Bug", "Hacking", "Cyber", "Code", "Robot", "Laboratory", "Ghost", "Burst", "Exocist",
    };
    string currentWord;
    int currentIndex;

    int score = 0;

    float timer = 5f;
    bool gameOver = false;

    //배열 or 텍스트 파일에서 텍스트 목록을 가져옴
    //랜덤으로 선택된 텍스트가 문제로 정의됨. (String q_txt = )
    //Input field에 텍스트를 적고 엔터를 누르면 답안 전송
    //Input field에 적힌 텍스트와 문제 텍스트가 같으면 다음 텍스트를 문제로 정의. 다르면 넘어가지 않음. (다시 Input field 활성화. 지우고 다시 쓰게끔)
    //한 문제당 5초(임시) 부여. 타이머가 0이 되면 게임 오버
    //문제가 넘어가면 타이머 리셋

    //5문제(임시) 정답 시 클리어


    void Start()
    {
        NextWord();
    }

    void Update()
    {
        if (!gameOver)
        {
            timer -= Time.deltaTime;
            txt_Timer.text = timer.ToString("n0");

            if (timer <= 0f)
            {
                gameOver = true;
                txt_Timer.text = "GameOver";
            }
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            Check();
        }

        if (score == 5)
        {
            Clear();
        }
    }

    void NextWord()
    {
        currentIndex = Random.Range(0, wordArray_Q.Length);
        currentWord = wordArray_Q[currentIndex];

        txt_Q.text = currentWord;

        inputfield.text = "";
        inputfield.ActivateInputField();

        timer = 5f;
    }

    void Check()
    {
        string inputText = inputfield.text;

        if (inputText.Equals(currentWord))
        {
            NextWord();
            Debug.Log("Good!");
            score++;
        }
        else
        {
            Debug.Log("Wrong");
        }
    }



    void Clear()
    {
        Time.timeScale = 0;
        txt_Q.text = "Clear!";
    }
}
