using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//현재 체력

    Transform player;
    public float shootDelay;
    public GameObject ghost_BulletPref;
    public Transform ghost_fireSpot;
    Vector2 ghost_movement;
    SpriteRenderer ghost_sprite;
    Rigidbody2D rigid;



    private void Start()
    {
        currentHealth = stats.health;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ghost_sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(repeat());
    }

    private void Update()
    {
        rigid.velocity = Vector2.zero;

        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            ghost_movement = direction;
        }

        ghost_Move(ghost_movement);

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

    void ghost_Move(Vector2 direction)
    {
        if (direction.x < 0) ghost_sprite.flipX = true;
        else ghost_sprite.flipX = false;
                  

    }

    IEnumerator repeat()
    {
        Instantiate(ghost_BulletPref, ghost_fireSpot.position, Quaternion.identity);
        Debug.Log("발사!");
        yield return new WaitForSeconds(shootDelay);
        StartCoroutine(repeat());
    }
}
