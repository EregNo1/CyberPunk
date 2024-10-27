using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//���� ü��

    [Header("������ ����")]
    public GameObject grossBullet; 
    public Transform grossBullet_TF;
    public SpriteRenderer grossBulletRenderer;
    public BoxCollider2D grossBullet_col;
    //public Animator grossBullet_Ani;
    float gb_angle = 0; //ȸ����
    float gb_length = 1f; //����

    public float detectionRange = 10f;
    public float attackDelay = 5f;

    private Transform player;
    Animator gross_animator;
    SpriteRenderer gross_Sprite;
    CapsuleCollider2D gross_col;
    Vector2 direction;
    float distance;

    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        gross_animator = GetComponent<Animator>();
        gross_col = GetComponent<CapsuleCollider2D>();
        gross_Sprite = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = stats.health;

        StartCoroutine(ChasePlayer());
    }

    // Update is called once per frame
    void Update()
    {

        if (!isAttacking)
        {
            Gross_Move(direction);
            direction = (player.position - transform.position).normalized;
        }
        else distanceCheck();

        // �÷��̾� ������ ���� ȸ��

    }

    private void FixedUpdate()
    {

    }

    //������ ������ ������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bujeok"))
        {
            TakeDamage(PlayerStats.Instance.attackDamage);
        }
    }

    //�÷��̾� ��ġ�� ���� �¿� �ø�
    void Gross_Move(Vector2 direction)
    {
        if (direction.x < 0) gross_Sprite.flipX = false;
        else gross_Sprite.flipX = true;
    }

    public void moveAni()
    {
        gross_animator.Play("Gross_Idle");
    }


    //������ ����
    void TakeDamage(float damage)
    {
        gross_animator.Play("Gross_Damaged");

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(gross_die());
    }

    IEnumerator gross_die()
    {
        gross_col.enabled = false;
        gross_animator.Play("Gross_Dead");
        RoomManager.instance.monsterDefeated();

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }

    void distanceCheck()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, LayerMask.GetMask("Border"));
        /*grossBulletRenderer.size = new Vector2(ray.distance, 0.85f);
        grossBullet_col.size = new Vector2(ray.distance, 0.33f);
        grossBullet_col.offset = new Vector2(ray.distance / 2 -1.4f, 0);*/
    }
    void SetScale()
    {
        gb_length = distance * 0.358f;
    }
    void SetAngle(Vector2 direction)
    {
        //�÷��̾ ���� ���� ���� ��, -x y 
        //�÷��̾ ���� �Ʒ��� ���� ��, -x -y
        //�÷��̾ ������ ���� ���� ��, x y
        //�÷��̾ ������ �Ʒ��� ���� ��, x -y

        if (direction.x < 0) //�÷��̾ ����
        {
            if (direction.y < 0) //�÷��̾ �Ʒ�
            {
                if (-direction.x > -direction.y)
                {
                    gb_angle = 0;
                    Debug.Log("���ʿ� ����� ���ʾƷ�");
                }
                else gb_angle = 90; Debug.Log("�Ʒ��ʿ� ����� ���ʾƷ�");
            }
            else //�÷��̾ ��
            {
                if (-direction.x > direction.y)
                {
                    gb_angle = 0;
                    Debug.Log("���ʿ� ����� ������");
                }
                else gb_angle = 270; Debug.Log("���ʿ� ����� ������");
            }
        }
        else //�÷��̾ ������
        {
            if (direction.y < 0) //�÷��̾ �Ʒ�
            {
                if (direction.x > -direction.y)
                {
                    gb_angle = 180;
                    Debug.Log("�����ʿ� ����� �����ʾƷ�");
                }
                else gb_angle = 90; Debug.Log("�Ʒ��ʿ� ����� �����ʾƷ�");
            }
            else //�÷��̾ ��
            {
                if (direction.x > direction.y)
                {
                    gb_angle = 180; Debug.Log("�����ʿ� ����� ��������");
                }
                else gb_angle = 270; Debug.Log("���ʿ� ����� ��������");
            }
        }

    }

    void FireLaser()
    {
        SetAngle(direction);

        gross_animator.Play("Gross_Attack");
        grossBullet.SetActive(true);
        grossBullet_TF.rotation = Quaternion.Euler(0, 0, gb_angle);
        grossBullet_TF.localScale = new Vector3(gb_length, 1, 1);
        //�������� ȸ������ ���� ����
    }



    // ���� ��ƾ
    private IEnumerator ChasePlayer()
    {
        while (true)
        {
            // 5�ʰ� �÷��̾� ����
            float chaseEndTime = Time.time + attackDelay;
            while (Time.time < chaseEndTime)
            {
                if (Vector2.Distance(transform.position, player.position) < detectionRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, stats.movementSpeed * Time.deltaTime);
                }
                yield return null;
            }

            // ���߰� ����
            isAttacking = true;
            yield return new WaitForSeconds(0.5f);
            FireLaser();
            yield return new WaitForSeconds(2f); // ������ ���� �ð�
            grossBullet.SetActive(false);

            isAttacking = false; // ���� �� �ٽ� ���� ���·� ��ȯ
        }
    }


}
