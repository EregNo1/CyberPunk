using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radial : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//현재 체력

    public float shootDelay;
    public float bulletSpeed = 5f;
    public int numberOfBullet = 8;
    public GameObject radial_BulletPref;
    public Transform radial_fireSpot;





    private void Start()
    {
        StartCoroutine(repeat());
        currentHealth = stats.health;
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

    void FireBullet()
    {
        float angleStep = 360f / numberOfBullet;

        for (int i = 0; i < numberOfBullet; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            Vector2 bulletDirection = rotation * Vector2.right;

            GameObject bullet = Instantiate(radial_BulletPref, radial_fireSpot.position, rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            bulletRb.velocity = bulletDirection.normalized * bulletSpeed;
        }
    }



    IEnumerator repeat()
    {
        FireBullet();
        yield return new WaitForSeconds(shootDelay);
        StartCoroutine(repeat());
    }
}
