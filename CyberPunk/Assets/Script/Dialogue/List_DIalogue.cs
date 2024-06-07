using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List_DIalogue : MonoBehaviour
{

    [SerializeField] DialogueList[] npc1_Minigame;
    [SerializeField] DialogueList[] npc2_Talk;
    [SerializeField] DialogueList[] desk_PortraitTest;

    
    public static bool isminigame = false;

    public void npc1_minigame()
    {
        DialogueManager.Instance.dialogueList = npc1_Minigame;
        DialogueManager.Instance.start_dialogue();
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
