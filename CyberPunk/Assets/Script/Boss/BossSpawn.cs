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
        //중보스 소환 ani 등 추가해서 코루틴으로 변경
        middleBoss.SetActive(true);
    }
}
