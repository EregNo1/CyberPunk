using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List_DIalogue : MonoBehaviour
{

    [SerializeField] DialogueList[] npc1_Minigame;
    [SerializeField] DialogueList[] npc2_Talk;
    [SerializeField] DialogueList[] desk_PortraitTest;

    
    public static bool isminigame = false; //미니게임 임시 실행. 나중에 수정될 것

    public void npc1_minigame()
    {
        DialogueManager.Instance.dialogueList = npc1_Minigame; //다이얼로그 매니저의 리스트를 npc1_Minigame 리스트로 변경
        DialogueManager.Instance.start_dialogue();//다이얼로그 실행
        isminigame=true;
        
    }
    public void npc2_talk()
    {
        DialogueManager.Instance.dialogueList = npc2_Talk;
        DialogueManager.Instance.start_dialogue();
    }
    public void desk_portraitTest()
    {
        DialogueManager.Instance.dialogueList = desk_PortraitTest;
        DialogueManager.Instance.start_dialogue();
    }


}
