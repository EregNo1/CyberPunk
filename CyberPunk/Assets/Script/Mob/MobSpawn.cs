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
        // ���� �����հ� ���� ��ġ�� �迭 ���̰� �ٸ��� ���� �α� ���
        if (monsterPrf_1phase.Length != monsterPrf_1phase.Length)
        {
            Debug.LogError("������1 ���� �迭�� ��ġ �迭�� ���̰� �ٸ��ϴ�!");
            return;
        }
        else if (monsterPrf_2phase.Length != monsterPrf_2phase.Length)
        {
            Debug.LogError("������2 ���� �迭�� ��ġ �迭�� ���̰� �ٸ��ϴ�!");
            return;
        }

        StartCoroutine(SpawnDelay());
        // �� ���� ��ġ�� �ش��ϴ� ���� �������� ��ȯ

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
