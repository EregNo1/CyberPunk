using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public MonsterStats stats;
    float currentHealth;//현재 체력

    [Header("레이저 관리")]
    public GameObject grossBullet; 
    public Transform grossBullet_TF;
    public SpriteRenderer grossBulletRenderer;
    public BoxCollider2D grossBullet_col;
    //public Animator grossBullet_Ani;
    float gb_angle = 0; //회전값
    float gb_length = 1f; //길이

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

        // 플레이어 방향을 향해 회전

    }

    private void FixedUpdate()
    {

    }

    //부적에 닿으면 데미지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bujeok"))
        {
            TakeDamage(PlayerStats.Instance.attackDamage);
        }
    }

    //플레이어 위치에 따라 좌우 플립
    void Gross_Move(Vector2 direction)
    {
        if (direction.x < 0) gross_Sprite.flipX = false;
        else gross_Sprite.flipX = true;
    }

    public void moveAni()
    {
        gross_animator.Play("Gross_Idle");
    }


    //데미지 받음
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
        //플레이어가 왼쪽 위에 있을 때, -x y 
        //플레이어가 왼쪽 아래에 있을 때, -x -y
        //플레이어가 오른쪽 위에 있을 때, x y
        //플레이어가 오른쪽 아래에 있을 때, x -y

        if (direction.x < 0) //플레이어가 왼쪽
        {
            if (direction.y < 0) //플레이어가 아래
            {
                if (-direction.x > -direction.y)
                {
                    gb_angle = 0;
                    Debug.Log("왼쪽에 가까운 왼쪽아래");
                }
                else gb_angle = 90; Debug.Log("아래쪽에 가까운 왼쪽아래");
            }
            else //플레이어가 위
            {
                if (-direction.x > direction.y)
                {
                    gb_angle = 0;
                    Debug.Log("왼쪽에 가까운 왼쪽위");
                }
                else gb_angle = 270; Debug.Log("위쪽에 가까운 왼쪽위");
            }
        }
        else //플레이어가 오른쪽
        {
            if (direction.y < 0) //플레이어가 아래
            {
                if (direction.x > -direction.y)
                {
                    gb_angle = 180;
                    Debug.Log("오른쪽에 가까운 오른쪽아래");
                }
                else gb_angle = 90; Debug.Log("아래쪽에 가까운 오른쪽아래");
            }
            else //플레이어가 위
            {
                if (direction.x > direction.y)
                {
                    gb_angle = 180; Debug.Log("오른쪽에 가까운 오른쪽위");
                }
                else gb_angle = 270; Debug.Log("위쪽에 가까운 오른쪽위");
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
        //레이저의 회전값과 길이 설정
    }



    // 공격 루틴
    private IEnumerator ChasePlayer()
    {
        while (true)
        {
            // 5초간 플레이어 추적
            float chaseEndTime = Time.time + attackDelay;
            while (Time.time < chaseEndTime)
            {
                if (Vector2.Distance(transform.position, player.position) < detectionRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, stats.movementSpeed * Time.deltaTime);
                }
                yield return null;
            }

            // 멈추고 공격
            isAttacking = true;
            yield return new WaitForSeconds(0.5f);
            FireLaser();
            yield return new WaitForSeconds(2f); // 레이저 유지 시간
            grossBullet.SetActive(false);

            isAttacking = false; // 공격 후 다시 추적 상태로 전환
        }
    }


}
