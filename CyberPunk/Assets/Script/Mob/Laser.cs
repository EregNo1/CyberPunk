using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//���� ü��

    public GameObject laserPrf; //������ ������
    public Transform firePoint; //�߻� ������
    public float shootDelay; //��� �� �� �ٽ� �� �������� �ð�

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
