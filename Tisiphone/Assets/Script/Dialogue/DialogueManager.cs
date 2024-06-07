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

    public int id;//���̾�α� ����


    //public static DialogueManager instance;

    // Start is called before the first frame update

    public void Awake()
    {
        //instance = this; 
        //DontDestroyOnLoad(this.gameObject);
    }



    public void next_dialogue() //���� ���̾�α׷� �Ѿ�� �Լ�
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

            //���� �迭��
            txt_contexts.text = dialogues[id];


            id++;

            StartCoroutine(Typing(txt_change));
        }
        else
        {
            DialogueSet.SetActive(false); //���� �迭�� ���� �� ���̾�α� â off
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
