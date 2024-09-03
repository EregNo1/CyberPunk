using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//���� ü��

    Transform player;
    Rigidbody2D bomber_rb;
    Vector2 bomber_movement;
    SpriteRenderer bomber_sprite;

    public float stopDis;
    public float explosionDelay;
    public float explosionRadius;

    public GameObject explosion;

    bool isExploding = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bomber_rb = GetComponent<Rigidbody2D>();
        bomber_sprite = GetComponent<SpriteRenderer>();
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
            if (distanceToPlayer > stopDis) //�Ÿ��� ������� ������ ��� �̵�
            {
                Bomber_Move(bomber_movement);
            }
            else //�Ÿ��� ��������� ����
            {
                StartCoroutine(Explode());
                
            }
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

    void Bomber_Move(Vector2 direction)
    {
        bomber_rb.velocity = direction * stats.movementSpeed;

        if (direction.x < 0) bomber_sprite.flipX = true;
        else bomber_sprite.flipX = false;
    }

    IEnumerator Explode()
    {
        isExploding = true;
        bomber_rb.velocity = Vector2.zero;
        float elapsed = 0f;
        bomber_sprite.color = Color.red;

        while (elapsed < explosionDelay)
        {
            elapsed += Time.deltaTime;

            if (Vector2.Distance(transform.position, player.position) > explosionRadius)
            {
                bomber_sprite.color = Color.white;
                isExploding = false;
                yield break;
            }

            yield return null;
        }
        //yield return new WaitForSeconds(explosionDelay);


        //���� �ִϸ��̼� ���
        yield return new WaitForSeconds(0.1f); //�ִϸ��̼��� �߰����� ����� ��
        explosion.SetActive(true); //���� ���� �ݶ��̴� Ȱ��ȭ
        yield return new WaitForSeconds(0.5f);//�ݶ��̴� ����

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
