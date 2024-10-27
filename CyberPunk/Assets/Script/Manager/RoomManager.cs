using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    int Phase1_monsterCount;
    int Phase2_monsterCount;
    GameObject[] exit;
    GameObject[] barrier;
    Animator[] door_Ani;

    public bool isPhase1 = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void savePhase1Info(int arrayLength)
    {
        Phase1_monsterCount = arrayLength;
    }

    public void saveRoomInfo(int arrayLength, GameObject[] exitArray, GameObject[] barrierArray, Animator[] doorAniArray) //트리거를 밟으면 배열 길이를 monsterCount에 저장 (MobSpawn스크립트)
    {
        Phase2_monsterCount = arrayLength;
        exit = exitArray;
        barrier = barrierArray;
        door_Ani = doorAniArray;

    }

    public void monsterDefeated() //몬스터가 죽으면 monsterCount -1 (몬스터 각자의 스크립트)
    {
        if (isPhase1 == true)
        {
            Phase1_monsterCount--;
            if (Phase1_monsterCount == 0)
            {

                isPhase1 = false;
            }
        }
        else
        {
            Phase2_monsterCount--;
            if (Phase2_monsterCount == 0)
            {
                foreach (var trigger in exit)
                {
                    trigger.SetActive(true);
                }
                foreach (var trigger in barrier)
                {
                    trigger.SetActive(false);
                }
                foreach (var trigger in door_Ani)
                {
                    trigger.Play("door_Open");
                }
                isPhase1 = true;
            }
        }

        
    }


}
