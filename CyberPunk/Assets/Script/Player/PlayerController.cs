using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public List_DIalogue list_Dialogue;
    public GameObject gameOver;

    float h;
    float v;
    public static bool ishit;

    public float speed;
    public float invincTime;
    public int currentHP = 2;

    public float knockBackSpeed;// 0 ~ 1 사이의 값
    public float knockBackDistance; //넉백 거리 
    public float knockBackDuration;
    float knockBackTime;
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

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //넉백이... 
        if (isKnockBack == true)
        {
            knockBackTime += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, knockBackPosition, knockBackSpeed);

            if (knockBackTime >= knockBackDuration || Vector2.Distance(transform.position, knockBackPosition) < 0.1f)
            {
                isKnockBack = false;
            }
        }

        //Space를 누르면 충돌한 오브젝트 이름을 스캔해 알맞은 다이얼로그 출력
        /*if (DialogueManager.Instance.istalk == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
            {
                Debug.Log("Hit :" + scanObject.name);
                DialogueAction();
                DialogueManager.Instance.istalk = true;
            }


        }*/
    }


    private void FixedUpdate()
    {
        if(ishit == false)
        {
            PlayerControll();//피격 애니메이션이 재생중인 동안은 이동 불가
        }

        /*Collider2D check = Physics2D.OverlapCircle(rigid.position, 1, LayerMask.GetMask("object")); //오브젝트 체크 (다이얼로그 출력용)
        Collider2D hit = Physics2D.OverlapCircle(rigid.position, 0.5f, LayerMask.GetMask("damage")); //히트 체크 (전투 전용)


        if (check != null) //충돌한 오브젝트가 check일 경우 
        {
            scanObject = check.gameObject; //scanObject에 충돌한 오브젝트 대입
        }
        else if (hit != null) //충돌한 오브젝트가 hit일 경우
        {
            scanObject = hit.gameObject;//scanObject에 충돌한 오브젝트 대입

            StartCoroutine(hit_Delay()); //히트 딜레이 코루틴(히트 함수 포함)
            
        }
        else //충돌한 것이 없으면 null 처리
        {
            scanObject = null;
        }*/


    }

    void OnTriggerEnter2D(Collider2D collision) //isTrigger가 켜져있는 오브젝트와 충돌
    {
        if (isinvinc == false)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
            {
                ishit = true; //이동 조작 막기
                isinvinc = true;
                KnockBack(collision.transform.position);
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
                ContactPoint2D contact = collision.GetContact(0);
                KnockBack(contact.point);
            }
        }

    }

    void KnockBack(Vector2 contactPoint)
    {

        if (contactPoint.x < transform.position.x)
        {

            if (currentHP <= 0)
            {
                anim.Play("Hero_DeadL");
                Die();
            }
            else
            {
                anim.Play("Hero_HitL");
                StartCoroutine(hit_Delay()); //애니메이션 종료, 이동조작 관리

                knockBackDetail((Vector3)contactPoint);

                currentHP--; //체력 감소
                Debug.Log("Player HP: " + currentHP);
            }
        }
        else
        {

            if (currentHP <= 0)
            {
                anim.Play("Hero_DeadR");
                Die();
            }
            else
            {
                anim.Play("Hero_HitR");
                StartCoroutine(hit_Delay()); //애니메이션 종료, 이동조작 관리

                knockBackDetail((Vector3)contactPoint);

                currentHP--; //체력 감소
                Debug.Log("Player HP: " + currentHP);
            }
        }




    }

    void knockBackDetail(Vector3 conP)
    {
        knockBackTime = 0;
        rigid.velocity = Vector2.zero;
        Vector2 dir = (transform.position - conP).normalized;
        knockBackPosition = (Vector2)transform.position + dir * knockBackDistance;
        isKnockBack = true;
    }

    void Die()
    {
        sprite.sortingOrder = 100;
        gameOver.SetActive(true);
        Time.timeScale = 0;
        //타임 정지
        //블랙아웃
    }


    //충돌 중인 오브젝트 이름에 따라 출력할 다이얼로그 결정
    //수정 필요...
    /*public void DialogueAction()
    {
        if (scanObject.name == "NPC_1") list_Dialogue.npc1_minigame();
        if (scanObject.name == "NPC_2") list_Dialogue.npc2_talk();
        if (scanObject.name == "frontdesk") list_Dialogue.desk_portraitTest();
    }*/


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
