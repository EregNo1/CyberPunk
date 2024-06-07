using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    [SerializeField] string csv_FileName;

    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public static bool isFinish = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Parser theParser = GetComponent<Parser>();
            Dialogue[] dialogues = theParser.Parse(csv_FileName);
            for(int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i+1, dialogues[i]);
            }
            isFinish = true;
        }
    }

    public Dialogue[] GetDialogues(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialgueList = new List<Dialogue>();

        for(int i = 0; i <= _EndNum - _StartNum; i++)
        {
            dialgueList.Add(dialogueDic[_StartNum + i]);
        }

        return dialgueList.ToArray();
    }
}
