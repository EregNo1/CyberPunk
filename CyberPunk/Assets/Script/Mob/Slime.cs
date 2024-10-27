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

    Animator slime_animator;
    CapsuleCollider2D slime_col;

    bool isDead = false;
    bool isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        slime_rb = GetComponent<Rigidbody2D>();
        slime_col = GetComponent<CapsuleCollider2D>();
        slime_sprite = GetComponent<SpriteRenderer>();
        slime_animator = GetComponent<Animator>();
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
        StartCoroutine(SlimeMove());
    }

    IEnumerator SlimeMove() {
        {
            if(isFirst) yield return new WaitForSeconds(1f);

            Slime_Move(slime_movement);
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
        slime_animator.Play("slimeA_Damaged");

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            slime_rb.velocity = Vector2.zero;
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(slime_die());
    }

    void Slime_Move(Vector2 direction)
    {
        if (!isDead)
        {
            slime_rb.velocity = direction * stats.movementSpeed;

            if (direction.x < 0) slime_sprite.flipX = true;
            else slime_sprite.flipX = false;
        }

    }

    public void moveAni()
    {
        slime_animator.Play("slimeA_Move");
    }
   

    IEnumerator slime_die()
    {
        slime_col.enabled = false;
        slime_animator.Play("slimeA_Dead");
        RoomManager.instance.monsterDefeated();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
