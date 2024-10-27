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

    Animator pong_animator;
    CircleCollider2D pong_col;

    private void Start()
    {
        currentHealth = stats.health;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ghost_sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        pong_animator = GetComponent<Animator>();
        pong_col = GetComponent<CircleCollider2D>();
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
        pong_animator.Play("pong_Damaged");

        currentHealth -= damage;
        if (currentHealth <= 0)

        {
            Die();
        }
    }

    void Die()
    {
                StopCoroutine(repeat());
        StartCoroutine(pong_die());
    }

    void ghost_Move(Vector2 direction)
    {
        if (direction.x < 0) ghost_sprite.flipX = true;
        else ghost_sprite.flipX = false;
                  

    }

    public void moveAni()
    {
        pong_animator.Play("mob1_Moving");
    }


    IEnumerator pong_die()
    {
        pong_col.enabled = false;
        pong_animator.Play("pong_Dead");
        RoomManager.instance.monsterDefeated();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }


    IEnumerator repeat()
    {
        yield return new WaitForSeconds(shootDelay);
        Instantiate(ghost_BulletPref, ghost_fireSpot.position, Quaternion.identity);
        Debug.Log("발사!");
        StartCoroutine(repeat());
    }


}
