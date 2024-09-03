using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//현재 체력

    Transform player;
    Rigidbody2D slime_rb;
    Vector2 slime_movement;
    SpriteRenderer slime_sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        slime_rb = GetComponent<Rigidbody2D>();
        slime_sprite = GetComponent<SpriteRenderer>();
        currentHealth = stats.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            slime_movement = direction;
        }
    }

    private void FixedUpdate()
    {
        Slime_Move(slime_movement);
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

    void Slime_Move(Vector2 direction)
    {
        slime_rb.velocity = direction * stats.movementSpeed;

        if (direction.x < 0) slime_sprite.flipX = true;
        else slime_sprite.flipX = false;
    }
}
