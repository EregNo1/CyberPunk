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
    [Header("�����Ű ���")]
    [SerializeField] GameObject DialogueSet;
    //[SerializeField] GameObject image_NamePanel;
        
    [SerializeField] UnityEngine.UI.Text txt_name;
    [SerializeField] UnityEngine.UI.Text txt_contexts;
    [SerializeField] UnityEngine.UI.Image img_Portrait;


    [Header("�ؽ�Ʈ ���ǵ�, Ÿ���� ���� Ȯ��")]
    [SerializeField] float txt_speed;
    public bool istyping;
    public bool istalk;

    [Header("���̾�α�")]
    [SerializeField] public DialogueList[] dialogueList;


    public testcodeMinigame test;





    string currentText;
    int id;//���̾�α� ����

    

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



    public void start_dialogue() //���̾�α׸� �����ϴ� �Լ�
    {


        id = 0;

        DialogueSet.SetActive(true);
        next_dialogue();



    }

    public void next_dialogue() //���� ���̾�α׷� �Ѿ�� �Լ�
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

            //���� �迭��
            txt_name.text = dialogueList[id].name;
            txt_contexts.text = dialogueList[id].contexts;
            img_Portrait.sprite = dialogueList[id].portrait;


            id++;

            StartCoroutine(Typing(txt_change)); 
        }
        else
        {
            
            DialogueSet.SetActive(false); //���� �迭�� ���� �� ���̾�α� â off
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
