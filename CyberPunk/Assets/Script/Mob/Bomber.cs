using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//현재 체력

    Transform player;
    Rigidbody2D bomber_rb;
    Vector2 bomber_movement;
    SpriteRenderer bomber_sprite;

    Animator creeper_animator;
    CapsuleCollider2D creeper_col;

    public float stopDis;
    public float explosionDelay;
    public float explosionRadius;

    public GameObject explosion_bound;
    public GameObject explosion;


    bool isExploding = false;
    bool isFirst = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bomber_rb = GetComponent<Rigidbody2D>();
        bomber_sprite = GetComponent<SpriteRenderer>();
        creeper_animator = GetComponent<Animator>();
        creeper_col = GetComponent<CapsuleCollider2D>();
        currentHealth = stats.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            bomber_movement = direction;
        }
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (!isExploding)
        {
            if (distanceToPlayer > stopDis) //거리가 가까워질 때까지 계속 이동
            {
                StartCoroutine(BomberMove());
            }
            else //거리가 가까워지면 멈춤
            {
                StartCoroutine(Explode());
                
            }
        }

    }
    IEnumerator BomberMove()
    {
        {
            if (isFirst) yield return new WaitForSeconds(1f);

            Bomber_Move(bomber_movement);
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
        creeper_animator.Play("Creeper_Damaged");

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine (chaser_die());
    }

    void Bomber_Move(Vector2 direction)
    {
        bomber_rb.velocity = direction * stats.movementSpeed;

        if (direction.x < 0) bomber_sprite.flipX = true;
        else bomber_sprite.flipX = false;
    }

    public void moveAni()
    {
        creeper_animator.Play("Creeper_Move");
    }


    IEnumerator chaser_die()
    {
        creeper_col.enabled = false;
        creeper_animator.Play("Creeper_Dead");
        RoomManager.instance.monsterDefeated();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    IEnumerator Explode()
    {
        creeper_animator.Play("Creeper_Delay");
        isExploding = true;
        bomber_rb.velocity = Vector2.zero;
        float elapsed = 0f;
        explosion_bound.SetActive(true);

        while (elapsed < explosionDelay)
        {

            elapsed += Time.deltaTime;

            if (Vector2.Distance(transform.position, player.position) > explosionRadius)
            {
                explosion_bound.SetActive(false);
                creeper_animator.Play("Creeper_Move");
                isExploding = false;
                yield break;
            }


            yield return null;
        }
        //yield return new WaitForSeconds(explosionDelay);


        explosion_bound.SetActive(false);
        creeper_animator.Play("Creeper_Bomb");
        //폭발 애니메이션 재생
        yield return new WaitForSeconds(0.1f); //애니메이션이 중간정도 진행된 후
        explosion.SetActive(true); //폭발 범위 콜라이더 활성화
        yield return new WaitForSeconds(0.5f);//콜라이더 유지

        RoomManager.instance.monsterDefeated();

        Destroy(gameObject);

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stopDis);
    }
}
