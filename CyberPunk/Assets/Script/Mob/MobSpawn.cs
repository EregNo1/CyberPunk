using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    public GameObject[] monsterPrf;
    public Transform[] spawnPoints;
    public GameObject[] transferTrigger;

    bool hasSpawned = false;


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasSpawned)
        {
            RoomManager.instance.saveRoomInfo(monsterPrf.Length, transferTrigger);
            closeExitTriggers();
            SpawnMonsters();
            hasSpawned = true;
        }
    }

    void SpawnMonsters()
    {
        // 몬스터 프리팹과 스폰 위치의 배열 길이가 다르면 에러 로그 출력
        if (monsterPrf.Length != spawnPoints.Length)
        {
            Debug.LogError("몬스터 배열과 위치 배열의 길이가 다릅니다!");
            return;
        }

        StartCoroutine(SpawnDelay());
        // 각 스폰 위치에 해당하는 몬스터 프리팹을 소환

    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < monsterPrf.Length; i++)
        {
            Instantiate(monsterPrf[i], spawnPoints[i].position, Quaternion.identity);
        }
    }


    private void closeExitTriggers()
    {
        foreach (var trigger in transferTrigger)
        {
            trigger.SetActive(false);
        }
    }
}
