using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

[System.Serializable]
public class DialogueList
{
    public string name;
    
    [TextArea]
    public string contexts;

    public Sprite portrait;
}

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance;
    [Header("히어라키 요소")]
    [SerializeField] GameObject DialogueSet;
    //[SerializeField] GameObject image_NamePanel;
        
    [SerializeField] UnityEngine.UI.Text txt_name;
    [SerializeField] UnityEngine.UI.Text txt_contexts;
    [SerializeField] UnityEngine.UI.Image img_Portrait;


    [Header("텍스트 스피드, 타이핑 여부 확인")]
    [SerializeField] float txt_speed;
    public bool istyping;
    public bool istalk;

    [Header("다이얼로그")]
    [SerializeField] public DialogueList[] dialogueList;


    public testcodeMinigame test;





    string currentText;
    int id;//다이얼로그 순서

    

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

       id = 0;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log(istalk);
        }

        if (istalk)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log(id);
                if (istyping)
                {
                    StopAllCoroutines();
                    txt_contexts.text = currentText;
                    istyping = false;
                }

                else
                {
                    next_dialogue();
                }
            }
        }



    }



    public void start_dialogue() //다이얼로그를 시작하는 함수
    {


        id = 0;

        DialogueSet.SetActive(true);
        next_dialogue();



    }

    public void next_dialogue() //다음 다이얼로그로 넘어가는 함수
    {
        Debug.Log("work");

        if (istyping)
        {
            StopAllCoroutines();
            txt_contexts.text = currentText;
            istyping = false;
        }

        else if (!istyping && id < dialogueList.Length)
        {
            string txt_change = dialogueList[id].contexts;

            //다음 배열로
            txt_name.text = dialogueList[id].name;
            txt_contexts.text = dialogueList[id].contexts;
            img_Portrait.sprite = dialogueList[id].portrait;


            id++;

            StartCoroutine(Typing(txt_change)); 
        }
        else
        {
            
            DialogueSet.SetActive(false); //다음 배열이 없을 시 다이얼로그 창 off
            StartCoroutine(istalking());
           if(List_DIalogue.isminigame == true)
            {
                test.buttonOn();
            }
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

    IEnumerator istalking()
    {
        yield return new WaitForSeconds(1f);
        istalk = false;
    }


}
