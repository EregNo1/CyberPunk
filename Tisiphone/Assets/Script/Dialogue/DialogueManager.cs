using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class DialogueManager : MonoBehaviour
{
    //public static DialogueManager Instance;

    [SerializeField] public GameObject DialogueSet;

    [SerializeField] TMP_Text txt_contexts;

    [SerializeField] float txt_speed;

    [SerializeField] public string[] dialogues = { };

    public GameObject barrier;
    public bool istyping;
    public string currentText;

    public int id;//다이얼로그 순서


    //public static DialogueManager instance;

    // Start is called before the first frame update

    public void Awake()
    {
        //instance = this; 
        //DontDestroyOnLoad(this.gameObject);
    }



    public void next_dialogue() //다음 다이얼로그로 넘어가는 함수
    {


        if (istyping)
        {
            StopAllCoroutines();
            txt_contexts.text = currentText;
            istyping = false;
        }

        else if (!istyping && id < dialogues.Length)
        {
            string txt_change = dialogues[id];

            //다음 배열로
            txt_contexts.text = dialogues[id];


            id++;

            StartCoroutine(Typing(txt_change));
        }
        else
        {
            DialogueSet.SetActive(false); //다음 배열이 없을 시 다이얼로그 창 off
            barrier.SetActive(false);
        }
    }

    IEnumerator Typing(string text)
    {
        txt_contexts.text = string.Empty;
        currentText = text;

        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);
            txt_contexts.text = stringBuilder.ToString();

            yield return new WaitForSeconds(txt_speed);
            istyping = true;
        }
        istyping = false;
    }
}
