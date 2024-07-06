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


        //Space�� ������ �浹�� ������Ʈ �̸��� ��ĵ�� �˸��� ���̾�α� ���
        if (DialogueManager.Instance.istalk == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
            {
                Debug.Log("Hit :" + scanObject.name);
                DialogueAction();
                DialogueManager.Instance.istalk = true;
            }


        }

        //HP�� 0�� �Ǹ� ��� �ִϸ��̼� ��� (���� ����)
        if(HP.hpNum == 0)
        {
            PlayerDead();
        }





    }


    ///OnTriggerStay2D �浹�ϰ� �ִ� ���� ����
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        collision.name = scanObject.name; //�浹�� �ݶ��̴� �̸��� scanObject �̸��� ����
    }*/

    private void FixedUpdate()
    {
        if(!ishit) PlayerControll();//�ǰ� �ִϸ��̼��� ������� ������ �̵� �Ұ�

        Collider2D check = Physics2D.OverlapCircle(rigid.position, 1, LayerMask.GetMask("object")); //������Ʈ üũ (���̾�α� ��¿�)
        Collider2D hit = Physics2D.OverlapCircle(rigid.position, 0.5f, LayerMask.GetMask("damage")); //��Ʈ üũ (���� ����)


        if (check != null) //�浹�� ������Ʈ�� check�� ��� 
        {
            scanObject = check.gameObject; //scanObject�� �浹�� ������Ʈ ����
            //Debug.Log(scanObject.name);
        }
        else if (hit != null) //�浹�� ������Ʈ�� hit�� ���
        {
            scanObject = hit.gameObject;//scanObject�� �浹�� ������Ʈ ����
            //Debug.Log(scanObject.name);

            Vector2 bulletDirection = Mob1Bullet.GetBulletDirection();
            StartCoroutine(hit_Delay()); //��Ʈ ������ �ڷ�ƾ(��Ʈ �Լ� ����)
            StartCoroutine(PlayerHit(bulletDirection, knockbackForce, knockbackDuration)); //��Ʈ ������ �ڷ�ƾ(��Ʈ �Լ� ����)
        }
        else //�浹�� ���� ������ null ó��
        {
            scanObject = null;
        }


    }



    //�浹 ���� ������Ʈ �̸��� ���� ����� ���̾�α� ����
    //���� �ʿ�...
    public void DialogueAction()
    {
        if (scanObject.name == "NPC_1") list_Dialogue.npc1_minigame();
        if (scanObject.name == "NPC_2") list_Dialogue.npc2_talk();
        if (scanObject.name == "frontdesk") list_Dialogue.desk_portraitTest();
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


    void PlayerDead()
    {
        rigid.velocity = Vector2.zero;
        anim.Play("Hero_DeadL");
    }


    //��Ʈ �Լ�


    IEnumerator PlayerHit(Vector2 direction, float force, float duration)
    {
        rigid.velocity = direction * force;
        yield return new WaitForSeconds(duration);
        rigid.velocity = Vector2.zero; //������ ����
    }

    //�ǰ� �� ������ �ڷ�ƾ. 
    IEnumerator hit_Delay()
    {
        ishit = true; //�̵� ���� ����


        if (Mob1Bullet.mob1_direction.x >= 0)
        {
            anim.Play("Hero_HitL"); //�ǰ� �ִϸ��̼� ���
        }
        else anim.Play("Hero_HitR"); //�ǰ� �ִϸ��̼� ���


        yield return new WaitForSeconds(0.7f); //�ִϸ��̼� ����ð� ���� ������ (�̵� ���� ����)
        anim.SetTrigger("hitEnd"); //�ǰ� �ִϸ��̼� ���� -> idle �ִϸ��̼� ���

        ishit = false; //�̵� ���� ����
    }

}
