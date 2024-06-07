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

    //�迭 or �ؽ�Ʈ ���Ͽ��� �ؽ�Ʈ ����� ������
    //�������� ���õ� �ؽ�Ʈ�� ������ ���ǵ�. (String q_txt = )
    //Input field�� �ؽ�Ʈ�� ���� ���͸� ������ ��� ����
    //Input field�� ���� �ؽ�Ʈ�� ���� �ؽ�Ʈ�� ������ ���� �ؽ�Ʈ�� ������ ����. �ٸ��� �Ѿ�� ����. (�ٽ� Input field Ȱ��ȭ. ����� �ٽ� ���Բ�)
    //�� ������ 5��(�ӽ�) �ο�. Ÿ�̸Ӱ� 0�� �Ǹ� ���� ����
    //������ �Ѿ�� Ÿ�̸� ����

    //5����(�ӽ�) ���� �� Ŭ����


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
