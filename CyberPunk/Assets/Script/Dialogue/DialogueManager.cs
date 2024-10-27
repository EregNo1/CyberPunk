using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

[System.Serializable]
public struct Speaker
{
    public UnityEngine.UI.Text txt_name;
    public UnityEngine.UI.Text txt_context;
    public UnityEngine.UI.Image img_portrait;
}

[System.Serializable]
public struct DialogueData
{
    public int speakerIndex;
    public Sprite portrait;
    public string name;
    [TextArea(3, 5)]
    public string dialogue;
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject DialogueSet;

    [SerializeField] private Speaker[] speakers;

    [SerializeField] private DialogueData[] dialogs;

    [SerializeField]
    private bool isAutoStart = true;

    public bool isFirst = true;
    public int currentTextIndex = -1;
    public int currentSpeakerIndex = 0;

    private float txt_speed = 0.1f;
    private bool istyping = false;


    public string currentText;


    public int id;//다이얼로그 순서



    private void Setup()
    {
        DialogueSet.SetActive(true);
    }

    public bool UpdateDialog()
    {
        if (isFirst == true)
        {

            Setup();

            if (isAutoStart) next_dialogue();

            isFirst = false;
            PlayerController.ishit = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (istyping == true)
            {
                istyping = false;

                StopCoroutine("OnTypingText");
                speakers[currentSpeakerIndex].txt_context.text = dialogs[currentTextIndex].dialogue;

                return false;
            }


            if (dialogs.Length > currentTextIndex + 1)
            {
                next_dialogue();
            }
            else
            {
                DialogueSet.SetActive(false);
                PlayerController.ishit = false;

                return true;
            }
        }

        return false;
    }

    public void next_dialogue() //다음 다이얼로그로 넘어가는 함수
    {
        //다음 대사 진행
        currentTextIndex++;

        //현재 화자 순번 설정
        currentSpeakerIndex = dialogs[currentTextIndex].speakerIndex;

        //현재 화자의 대사 텍스트 설정
        StartCoroutine("OnTypingText");
        //speakers[currentSpeakerIndex].txt_context.text = dialogs[currentTextIndex].dialogue;

        //현재 화자의 이름 텍스트 설정
        speakers[currentSpeakerIndex].txt_name.text = dialogs[currentTextIndex].name;

        //현재 화자의 초상화 이미지 설정
        speakers[currentSpeakerIndex].img_portrait.sprite = dialogs[currentTextIndex].portrait;


    }

    IEnumerator OnTypingText()
    {
        int index = 0;

        istyping = true;

        while (index <= dialogs[currentTextIndex].dialogue.Length)
        {
            speakers[currentSpeakerIndex].txt_context.text = dialogs[currentTextIndex].dialogue.Substring(0, index);
            index++;
            yield return new WaitForSeconds(txt_speed);
        }

        istyping = false;
    }
}
