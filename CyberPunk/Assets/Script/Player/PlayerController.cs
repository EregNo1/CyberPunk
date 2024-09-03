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

    public float knockBackSpeed;// 0 ~ 1 ������ ��
    public float knockBackDistance; //�˹� �Ÿ� 
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

        //�˹���... 
        if (isKnockBack == true)
        {
            knockBackTime += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, knockBackPosition, knockBackSpeed);

            if (knockBackTime >= knockBackDuration || Vector2.Distance(transform.position, knockBackPosition) < 0.1f)
            {
                isKnockBack = false;
            }
        }

        //Space�� ������ �浹�� ������Ʈ �̸��� ��ĵ�� �˸��� ���̾�α� ���
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
            PlayerControll();//�ǰ� �ִϸ��̼��� ������� ������ �̵� �Ұ�
        }

        /*Collider2D check = Physics2D.OverlapCircle(rigid.position, 1, LayerMask.GetMask("object")); //������Ʈ üũ (���̾�α� ��¿�)
        Collider2D hit = Physics2D.OverlapCircle(rigid.position, 0.5f, LayerMask.GetMask("damage")); //��Ʈ üũ (���� ����)


        if (check != null) //�浹�� ������Ʈ�� check�� ��� 
        {
            scanObject = check.gameObject; //scanObject�� �浹�� ������Ʈ ����
        }
        else if (hit != null) //�浹�� ������Ʈ�� hit�� ���
        {
            scanObject = hit.gameObject;//scanObject�� �浹�� ������Ʈ ����

            StartCoroutine(hit_Delay()); //��Ʈ ������ �ڷ�ƾ(��Ʈ �Լ� ����)
            
        }
        else //�浹�� ���� ������ null ó��
        {
            scanObject = null;
        }*/


    }

    void OnTriggerEnter2D(Collider2D collision) //isTrigger�� �����ִ� ������Ʈ�� �浹
    {
        if (isinvinc == false)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Damage"))
            {
                ishit = true; //�̵� ���� ����
                isinvinc = true;
                KnockBack(collision.transform.position);
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
                StartCoroutine(hit_Delay()); //�ִϸ��̼� ����, �̵����� ����

                knockBackDetail((Vector3)contactPoint);

                currentHP--; //ü�� ����
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
                StartCoroutine(hit_Delay()); //�ִϸ��̼� ����, �̵����� ����

                knockBackDetail((Vector3)contactPoint);

                currentHP--; //ü�� ����
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
        //Ÿ�� ����
        //���ƿ�
    }


    //�浹 ���� ������Ʈ �̸��� ���� ����� ���̾�α� ����
    //���� �ʿ�...
    /*public void DialogueAction()
    {
        if (scanObject.name == "NPC_1") list_Dialogue.npc1_minigame();
        if (scanObject.name == "NPC_2") list_Dialogue.npc2_talk();
        if (scanObject.name == "frontdesk") list_Dialogue.desk_portraitTest();
    }*/


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
