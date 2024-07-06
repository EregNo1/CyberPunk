using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public List_DIalogue list_Dialogue;

    // Start is called before the first frame update
    float h;
    float v;
    bool isHorizonMove;
    bool ishit;

    public float speed;
    public float knockbackForce;
    public float knockbackDuration;

    Rigidbody2D rigid;

    Animator anim;

    SpriteRenderer sprite;

    Collider2D col2D;

    Vector2 dirVec;


    GameObject scanObject;




    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = rigid.GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col2D = GetComponent<Collider2D>();

    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");


        //Space를 누르면 충돌한 오브젝트 이름을 스캔해 알맞은 다이얼로그 출력
        if (DialogueManager.Instance.istalk == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
            {
                Debug.Log("Hit :" + scanObject.name);
                DialogueAction();
                DialogueManager.Instance.istalk = true;
            }


        }

        //HP가 0이 되면 사망 애니메이션 재생 (추후 수정)
        if(HP.hpNum == 0)
        {
            PlayerDead();
        }





    }


    ///OnTriggerStay2D 충돌하고 있는 동안 실행
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        collision.name = scanObject.name; //충돌한 콜라이더 이름을 scanObject 이름에 대입
    }*/

    private void FixedUpdate()
    {
        if(!ishit) PlayerControll();//피격 애니메이션이 재생중인 동안은 이동 불가

        Collider2D check = Physics2D.OverlapCircle(rigid.position, 1, LayerMask.GetMask("object")); //오브젝트 체크 (다이얼로그 출력용)
        Collider2D hit = Physics2D.OverlapCircle(rigid.position, 0.5f, LayerMask.GetMask("damage")); //히트 체크 (전투 전용)


        if (check != null) //충돌한 오브젝트가 check일 경우 
        {
            scanObject = check.gameObject; //scanObject에 충돌한 오브젝트 대입
            //Debug.Log(scanObject.name);
        }
        else if (hit != null) //충돌한 오브젝트가 hit일 경우
        {
            scanObject = hit.gameObject;//scanObject에 충돌한 오브젝트 대입
            //Debug.Log(scanObject.name);

            Vector2 bulletDirection = Mob1Bullet.GetBulletDirection();
            StartCoroutine(hit_Delay()); //히트 딜레이 코루틴(히트 함수 포함)
            StartCoroutine(PlayerHit(bulletDirection, knockbackForce, knockbackDuration)); //히트 딜레이 코루틴(히트 함수 포함)
        }
        else //충돌한 것이 없으면 null 처리
        {
            scanObject = null;
        }


    }



    //충돌 중인 오브젝트 이름에 따라 출력할 다이얼로그 결정
    //수정 필요...
    public void DialogueAction()
    {
        if (scanObject.name == "NPC_1") list_Dialogue.npc1_minigame();
        if (scanObject.name == "NPC_2") list_Dialogue.npc2_talk();
        if (scanObject.name == "frontdesk") list_Dialogue.desk_portraitTest();
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


    void PlayerDead()
    {
        rigid.velocity = Vector2.zero;
        anim.Play("Hero_DeadL");
    }


    //히트 함수


    IEnumerator PlayerHit(Vector2 direction, float force, float duration)
    {
        rigid.velocity = direction * force;
        yield return new WaitForSeconds(duration);
        rigid.velocity = Vector2.zero; //움직임 멈춤
    }

    //피격 후 딜레이 코루틴. 
    IEnumerator hit_Delay()
    {
        ishit = true; //이동 조작 막기


        if (Mob1Bullet.mob1_direction.x >= 0)
        {
            anim.Play("Hero_HitL"); //피격 애니메이션 재생
        }
        else anim.Play("Hero_HitR"); //피격 애니메이션 재생


        yield return new WaitForSeconds(0.7f); //애니메이션 재생시간 동안 딜레이 (이동 조작 막기)
        anim.SetTrigger("hitEnd"); //피격 애니메이션 종료 -> idle 애니메이션 재생

        ishit = false; //이동 조작 해제
    }

}
