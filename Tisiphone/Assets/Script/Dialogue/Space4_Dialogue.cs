using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space4_Dialogue : DialogueManager
{
    bool isopen = false;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isopen == true)
            {
                next_dialogue();
            }
            if (id > dialogues.Length)
            {
                isopen = false;
            }
            
        }
    }

    public void start_dialogue()
    {
        DialogueSet.SetActive(true);
        isopen = true;
        barrier.SetActive(true);
        id = 0;
        next_dialogue();

    }

    public void window()
    {
        string[] Dialogue_Window = { "유리가 검은 창문이다." , "...자세히 보니 창문 너머가 벽으로 막혀있다."};
        dialogues = Dialogue_Window;
        
        start_dialogue();
    }


    public void sofaL()
    {
        string[] Dialogue_sofaL = { "잘 모르지만, 고급져 보이는 소파이다." };
        dialogues = Dialogue_sofaL;

        start_dialogue();
    }
}
