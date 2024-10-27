using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnActive : MonoBehaviour
{
    public GameObject mobspawn_GO;
    public MobSpawn mobspawn;

    bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasSpawned)
        {
            mobspawn_GO.SetActive(true);
            mobspawn.saveInfo();
            hasSpawned = true;
        }
    }
}
