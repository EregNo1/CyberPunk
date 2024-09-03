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
        // ���� �����հ� ���� ��ġ�� �迭 ���̰� �ٸ��� ���� �α� ���
        if (monsterPrf.Length != spawnPoints.Length)
        {
            Debug.LogError("���� �迭�� ��ġ �迭�� ���̰� �ٸ��ϴ�!");
            return;
        }

        StartCoroutine(SpawnDelay());
        // �� ���� ��ġ�� �ش��ϴ� ���� �������� ��ȯ

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
