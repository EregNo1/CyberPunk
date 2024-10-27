using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radial : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//현재 체력

    Transform player;
    public float shootDelay;
    public float bulletSpeed = 5f;
    public int numberOfBullet = 8;
    public GameObject radial_BulletPref;
    public Transform radial_fireSpot;
    Vector2 radial_movement;
    Rigidbody2D rigid;
    SpriteRenderer radial_sprite;

    Animator radial_animator;
    CapsuleCollider2D radial_col;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        radial_sprite = GetComponent<SpriteRenderer>();
        radial_animator = GetComponent<Animator>();
        radial_col = GetComponent<CapsuleCollider2D>();
        StartCoroutine(repeat());
        currentHealth = stats.health;
    }

    private void Update()
    {

        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            radial_movement = direction;
        }

        radial_Move(radial_movement);
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
        radial_animator.Play("Radial_Damaged");

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        StopCoroutine(repeat());
        StartCoroutine(radial_die());
    }

    void radial_Move(Vector2 direction)
    {
        if (direction.x < 0) radial_sprite.flipX = true;
        else radial_sprite.flipX = false;


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

    public void moveAni()
    {
        radial_animator.Play("Radial_Move");
    }


    IEnumerator radial_die()
    {

        radial_col.enabled = false;
        radial_animator.Play("Radial_Dead");
        RoomManager.instance.monsterDefeated();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    IEnumerator repeat()
    {
        yield return new WaitForSeconds(shootDelay);
        FireBullet();
        StartCoroutine(repeat());
    }
}
