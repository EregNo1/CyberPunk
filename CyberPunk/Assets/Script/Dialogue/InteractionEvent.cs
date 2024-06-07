using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogue;

    public Dialogue[] GetDialogues()
    {
        dialogue.dialogues = DatabaseManager.Instance.GetDialogues((int)dialogue.line.x, (int)dialogue.line.y);
        return dialogue.dialogues;
    }
}
