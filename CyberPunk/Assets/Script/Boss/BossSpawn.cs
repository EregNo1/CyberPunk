using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    public GameObject middleBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Spawn_MiddleBoss();
        }
    }

    public void Spawn_MiddleBoss()
    {
        //�ߺ��� ��ȯ ani �� �߰��ؼ� �ڷ�ƾ���� ����
        middleBoss.SetActive(true);
    }
}
