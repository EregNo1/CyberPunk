using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//현재 체력

    public GameObject laserPrf; //레이저 프리팹
    public Transform firePoint; //발사 시작점
    public float shootDelay; //쏘고 난 후 다시 쏠 때까지의 시간

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = stats.health;
        StartCoroutine(repeat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bujeok"))
        {
            TakeDamage(PlayerStats.Instance.attackDamage);
        }
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        RoomManager.instance.monsterDefeated();
        Destroy(gameObject);
    }
    void FireLaser()
    {
        Instantiate(laserPrf, firePoint.position, firePoint.rotation);
    }

    IEnumerator repeat()
    {
        FireLaser();
        yield return new WaitForSeconds(shootDelay);
        StartCoroutine(repeat());
    }

}
