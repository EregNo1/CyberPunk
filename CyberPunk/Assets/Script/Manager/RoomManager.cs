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

    public void saveRoomInfo(int arrayLength, GameObject[] exitArray) //Ʈ���Ÿ� ������ �迭 ���̸� monsterCount�� ���� (MobSpawn��ũ��Ʈ)
    {
        monsterCount = arrayLength;
        exit = exitArray;
    }

    public void monsterDefeated() //���Ͱ� ������ monsterCount -1 (���� ������ ��ũ��Ʈ)
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
