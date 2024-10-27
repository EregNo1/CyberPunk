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

    Animator chaser_animator;
    CapsuleCollider2D chaser_col;

    bool isDead = false;
    bool isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        chaser_rb = GetComponent<Rigidbody2D>();
        chaser_sprite = GetComponent<SpriteRenderer>();
        chaser_animator = GetComponent<Animator>();
        chaser_col = GetComponent<CapsuleCollider2D>();
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
        StartCoroutine(ChaserMove());
    }

    IEnumerator ChaserMove()
    {
        {
            if (isFirst) yield return new WaitForSeconds(1f);

            Chaser_Move(chaser_movement);
            isFirst = false;
        }
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
        chaser_animator.Play("Chaser_Damaged");

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            chaser_rb.velocity = Vector2.zero;
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(chaser_die());
    }
    void Chaser_Move(Vector2 direction)
    {
        if (!isDead)
        {
            chaser_rb.velocity = direction * stats.movementSpeed;

            if (direction.x < 0) chaser_sprite.flipX = true;
            else chaser_sprite.flipX = false;
        }

    }


    public void moveAni()
    {
        chaser_animator.Play("Chaser_Move");
    }


    IEnumerator chaser_die()
    {
        chaser_col.enabled = false;
        chaser_animator.Play("Chaser_Dead");
        RoomManager.instance.monsterDefeated();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
