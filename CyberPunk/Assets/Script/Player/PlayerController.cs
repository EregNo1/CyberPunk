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

    public float knockBackSpeed;// 0 ~ 1 ������ ��
    public float knockBackDistance; //�˹� �Ÿ� 
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
                rigid.velocity = Vector2.zero;  // �з��� �� ����
            }
        }

    }


    private void FixedUpdate()
    {
        if (isKnockBack) return;
        if(ishit == false)
        {
            PlayerControll();//�ǰ� �ִϸ��̼��� ������� ������ �̵� �Ұ�
        }

    }

    void OnTriggerEnter2D(Collider2D collision) //isTrigger�� �����ִ� ������Ʈ�� �浹
    {
        if (isinvinc == false)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
            {
                ishit = true; //�̵� ���� ����
                isinvinc = true;
                KnockBack(collision.transform.position); //KnockBack�Լ��� ���� ��ġ ����
            }
        }

    }
    void OnCollisionEnter2D(Collision2D collision) //isTrigger�� �����ִ� ������Ʈ�� �浹
    {
        if (isinvinc == false)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
            {
                ishit = true; //�̵� ���� ����
                isinvinc = true;
                ContactPoint2D contact = collision.GetContact(0); //contact�� ���� ��ġ ����
                KnockBack(contact.point);//KnockBack�Լ��� ���� ��ġ ����
            }
        }

    }

    void KnockBack(Vector2 contactPoint)
    {

        if (contactPoint.x < transform.position.x) //���ʿ��� �ε���
        {

            if (hpManager.hpNum <= 1) //hp�� 0�� �� �� ��� �ִϸ��̼� ���
            {
                anim.Play("Hero_DeadL");
                Die();
            }
            else
            {
                hpManager.damage_Ani(); //������ ȿ�� �ִϸ��̼� ���
                anim.Play("Hero_HitL"); //ĳ���� �ǰ� �ִϸ��̼� ���
                StartCoroutine(hit_Delay()); //�ִϸ��̼� ����, �̵����� ����

                knockBackDetail((Vector3)contactPoint);//knockBackDetail�Լ��� ���� ��ġ ���� (�˹� ����)


                hpManager.hpNum--; //ü�� ����
                hpManager.hpUpdate(); //ü�� UI ������Ʈ
            }
        }
        else //�����ʿ��� �ε���
        {

            if (hpManager.hpNum <= 1) //hp�� 0�� �� �� ��� �ִϸ��̼� ���
            {
                anim.Play("Hero_DeadR");
                Die();
            }
            else
            {
                hpManager.damage_Ani(); //������ ȿ�� �ִϸ��̼� ���
                anim.Play("Hero_HitR"); //ĳ���� �ǰ� �ִϸ��̼� ���
                StartCoroutine(hit_Delay()); //�ִϸ��̼� ����, �̵����� ����

                knockBackDetail((Vector3)contactPoint); //knockBackDetail�Լ��� ���� ��ġ ���� (�˹� ����)


                hpManager.hpNum--; //ü�� ����
                hpManager.hpUpdate(); //ü�� UI ������Ʈ
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
        //Ÿ�� ����
        //���ƿ�
    }




    //�÷��̾� �̵� �Լ�
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
        isKnockBack = true; // �˹� �� ����

        // �˹� �ӵ� ����
        rigid.velocity = dir * knockBackSpeed;

        // �˹� �ð� ���� ���
        yield return new WaitForSeconds(knockBackDistance / knockBackSpeed);

        // �ӵ� �ʱ�ȭ �� �˹� ����
        rigid.velocity = Vector2.zero;
        isKnockBack = false;
    }

    //�ǰ� �� ������ �ڷ�ƾ. 
    IEnumerator hit_Delay()
    {

        yield return new WaitForSeconds(0.7f); //�ִϸ��̼� ����ð� ���� ������ (�̵� ���� ����)
        anim.SetTrigger("hitEnd"); //�ǰ� �ִϸ��̼� ���� -> idle �ִϸ��̼� ���

        ishit = false; //�̵� ���� ����

        yield return new WaitForSeconds(invincTime);
        isinvinc = false;
    }

}
