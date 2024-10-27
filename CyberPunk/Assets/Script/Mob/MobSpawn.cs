using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public GameObject[] monsterPrf_1phase;
    public Transform[] spawnPoints_1phase;
    public GameObject[] monsterPrf_2phase;
    public Transform[] spawnPoints_2phase;

    public GameObject[] transferTrigger;
    public GameObject[] doorBarrier;
    public Animator[] doorAni;

    bool hasSpawned = false;
    bool phase1Clear = false;

    private void Update()
    {
        if (!phase1Clear)
        {
            if (RoomManager.instance.isPhase1 == false)
            {
                SpawnMonsters();
                phase1Clear = true;
            }

        }
    }



    public void saveInfo()
    {
        RoomManager.instance.savePhase1Info(monsterPrf_1phase.Length);
        RoomManager.instance.saveRoomInfo(monsterPrf_2phase.Length, transferTrigger, doorBarrier, doorAni);
        closeExitTriggers();
        SpawnMonsters();
        hasSpawned = true;
    }

    void SpawnMonsters()
    {
        // 몬스터 프리팹과 스폰 위치의 배열 길이가 다르면 에러 로그 출력
        if (monsterPrf_1phase.Length != monsterPrf_1phase.Length)
        {
            Debug.LogError("페이즈1 몬스터 배열과 위치 배열의 길이가 다릅니다!");
            return;
        }
        else if (monsterPrf_2phase.Length != monsterPrf_2phase.Length)
        {
            Debug.LogError("페이즈2 몬스터 배열과 위치 배열의 길이가 다릅니다!");
            return;
        }

        StartCoroutine(SpawnDelay());
        // 각 스폰 위치에 해당하는 몬스터 프리팹을 소환

    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1f);

        if (RoomManager.instance.isPhase1 == true)
        {
            for (int i = 0; i < monsterPrf_1phase.Length; i++)
            {
                Instantiate(monsterPrf_1phase[i], spawnPoints_1phase[i].position, Quaternion.identity);
            }

            foreach (var trigger in doorAni)
            {
                trigger.Play("door_Close");
            }
        }
        else
        {
            for (int i = 0; i < monsterPrf_2phase.Length; i++)
            {
                Instantiate(monsterPrf_2phase[i], spawnPoints_2phase[i].position, Quaternion.identity);
            }
        }


    }


    private void closeExitTriggers()
    {
        foreach (var trigger in transferTrigger)
        {
            trigger.SetActive(false);
        }
        foreach (var trigger in doorBarrier)
        {
            trigger.SetActive(true);
        }

    }
}
