using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;
    int monsterCount;
    GameObject[] exit;

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

    public void saveRoomInfo(int arrayLength, GameObject[] exitArray) //트리거를 밟으면 배열 길이를 monsterCount에 저장 (MobSpawn스크립트)
    {
        monsterCount = arrayLength;
        exit = exitArray;
    }

    public void monsterDefeated() //몬스터가 죽으면 monsterCount -1 (몬스터 각자의 스크립트)
    {
        monsterCount--;
        if (monsterCount == 0)
        {
            foreach (var trigger in exit)
            {
                trigger.SetActive(true);
            }
        }
    }


}
