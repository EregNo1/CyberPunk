using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialog_Test : MonoBehaviour
{
    [SerializeField]
    private DialogueManager testDialougue;


    public void play_test()
    {
        StartCoroutine(testDialogue());
    }

    IEnumerator testDialogue()
    {
        yield return new WaitForSeconds(0.5f);

        yield return new WaitUntil(() => testDialougue.UpdateDialog());

        yield return new WaitForSeconds(1f);
    }
}
