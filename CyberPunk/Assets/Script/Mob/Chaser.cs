using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//현재 체력

    Transform player;
    Rigidbody2D chaser_rb;
    Vector2 chaser_movement;
    SpriteRenderer chaser_sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        chaser_rb = GetComponent<Rigidbody2D>();
        chaser_sprite = GetComponent<SpriteRenderer>();
        currentHealth = stats.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            chaser_movement = direction;
        }
    }

    private void FixedUpdate()
    {
        Chaser_Move(chaser_movement);
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
    void Chaser_Move(Vector2 direction)
    {
        chaser_rb.velocity = direction * stats.movementSpeed;

        if (direction.x < 0) chaser_sprite.flipX = true;
        else chaser_sprite.flipX = false;
    }
}
