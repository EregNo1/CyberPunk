using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public List_DIalogue list_Dialogue;
    public HP hpManager;
    public GameObject gameOver;


    float h;
    float v;
    public static bool ishit;
    public static bool keySwap = false;

    public float speed;
    public float invincTime;

    public float knockBackSpeed;// 0 ~ 1 사이의 값
    public float knockBackDistance; //넉백 거리 
    Vector2 knockBackPosition;
    bool isKnockBack = false;




    bool isinvinc;
    Animator anim;
    SpriteRenderer sprite;
    Collider2D col2D;
    Vector2 dirVec;
    GameObject scanObject;

    public static Rigidbody2D rigid;




    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();

    }


    void Update()
    {
        if (keySwap == false)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal_swap");
            v = Input.GetAxisRaw("Vertical_swap");
        }

        if (isKnockBack)
        {
            float distanceMoved = Vector2.Distance(rigid.position, rigid.position + knockBackPosition);
            if (distanceMoved >= knockBackDistance)
            {
                isKnockBack = false;
                rigid.velocity = Vector2.zero;  // 밀려난 후 멈춤
            }
        }

    }


    private void FixedUpdate()
    {
        if (isKnockBack) return;
        if(ishit == false)
        {
            PlayerControll();//피격 애니메이션이 재생중인 동안은 이동 불가
        }

    }

    void OnTriggerEnter2D(Collider2D collision) //isTrigger가 켜져있는 오브젝트와 충돌
    {
        if (isinvinc == false)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
            {
                ishit = true; //이동 조작 막기
                isinvinc = true;
                KnockBack(collision.transform.position); //KnockBack함수에 닿은 위치 전달
            }
        }

    }
    void OnCollisionEnter2D(Collision2D collision) //isTrigger가 꺼져있는 오브젝트와 충돌
    {
        if (isinvinc == false)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
            {
                ishit = true; //이동 조작 막기
                isinvinc = true;
                ContactPoint2D contact = collision.GetContact(0); //contact에 닿은 위치 저장
                KnockBack(contact.point);//KnockBack함수에 닿은 위치 전달
            }
        }

    }

    void KnockBack(Vector2 contactPoint)
    {

        if (contactPoint.x < transform.position.x) //왼쪽에서 부딪힘
        {

            if (hpManager.hpNum <= 1) //hp가 0이 될 시 사망 애니메이션 재생
            {
                anim.Play("Hero_DeadL");
                Die();
            }
            else
            {
                hpManager.damage_Ani(); //데미지 효과 애니메이션 재생
                anim.Play("Hero_HitL"); //캐릭터 피격 애니메이션 재생
                StartCoroutine(hit_Delay()); //애니메이션 종료, 이동조작 관리

                knockBackDetail((Vector3)contactPoint);//knockBackDetail함수에 닿은 위치 전달 (넉백 실행)


                hpManager.hpNum--; //체력 감소
                hpManager.hpUpdate(); //체력 UI 업데이트
            }
        }
        else //오른쪽에서 부딪힘
        {

            if (hpManager.hpNum <= 1) //hp가 0이 될 시 사망 애니메이션 재생
            {
                anim.Play("Hero_DeadR");
                Die();
            }
            else
            {
                hpManager.damage_Ani(); //데미지 효과 애니메이션 재생
                anim.Play("Hero_HitR"); //캐릭터 피격 애니메이션 재생
                StartCoroutine(hit_Delay()); //애니메이션 종료, 이동조작 관리

                knockBackDetail((Vector3)contactPoint); //knockBackDetail함수에 닿은 위치 전달 (넉백 실행)


                hpManager.hpNum--; //체력 감소
                hpManager.hpUpdate(); //체력 UI 업데이트
            }
        }




    }

    void knockBackDetail(Vector2 conP)
    {
        Vector2 knockBackPosition = (rigid.position - conP).normalized;

        StartCoroutine(knockBackCoroutine(knockBackPosition));

        /*isKnockBack = true;
        rigid.velocity = knockBackPosition;*/
    }

    void Die()
    {
        sprite.sortingOrder = 100;
        gameOver.SetActive(true);
        Time.timeScale = 0;
        //타임 정지
        //블랙아웃
    }




    //플레이어 이동 함수
    void PlayerControll()
    {
        Vector2 moveVec = new Vector2(h, v);
        rigid.velocity = moveVec.normalized * speed;


        if (moveVec.x > 0)
        {
            dirVec = Vector3.right;
            anim.SetInteger("isHorizontal", 1);
            anim.SetBool("isMove", true);
        }
        else if (moveVec.x < 0)
        {
            dirVec = Vector3.left;
            anim.SetInteger("isHorizontal", -1);
            anim.SetBool("isMove", true);
        }
        else if (moveVec.y < 0)
        {
            anim.SetBool("isMove", true);
        }
        else if (moveVec.y > 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetInteger("isHorizontal", 0);
            anim.SetBool("isMove", false);
        }
    }

    IEnumerator knockBackCoroutine(Vector2 dir)
    {
        isKnockBack = true; // 넉백 중 상태

        // 넉백 속도 적용
        rigid.velocity = dir * knockBackSpeed;

        // 넉백 시간 동안 대기
        yield return new WaitForSeconds(knockBackDistance / knockBackSpeed);

        // 속도 초기화 및 넉백 종료
        rigid.velocity = Vector2.zero;
        isKnockBack = false;
    }

    //피격 후 딜레이 코루틴. 
    IEnumerator hit_Delay()
    {

        yield return new WaitForSeconds(0.7f); //애니메이션 재생시간 동안 딜레이 (이동 조작 막기)
        anim.SetTrigger("hitEnd"); //피격 애니메이션 종료 -> idle 애니메이션 재생

        ishit = false; //이동 조작 해제

        yield return new WaitForSeconds(invincTime);
        isinvinc = false;
    }

}
