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

    public float speed;

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


        if (DialogueManager.Instance.istalk == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
            {
                Debug.Log("Hit :" + scanObject.name);
                DialogueAction();
                DialogueManager.Instance.istalk = true;
            }


        }








    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.name = scanObject.name;
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.name = scanObject.name;
    }

    private void FixedUpdate()
    {
        PlayerControll();

        Collider2D check = Physics2D.OverlapCircle(rigid.position, 1, LayerMask.GetMask("object"));
        //Collider2D hit = Physics2D.OverlapCircle(rigid.position, 0.4f, LayerMask.GetMask("damage"));


        if (check != null)
        {
            scanObject = check.gameObject;
            Debug.Log(scanObject.name);
        }

        /*if (hit != null)
        {
            scanObject = hit.gameObject;
            //hit_Delay();

            Debug.Log("HitDAmage");
        }*/

        /*Debug.DrawRay(rigid.position, dirVec, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 1f, LayerMask.GetMask("object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }*/

    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rigid.position, 1);
    }*/

    public void DialogueAction()
    {
        if (scanObject.name == "NPC_1") list_Dialogue.npc1_minigame();
        if (scanObject.name == "NPC_2") list_Dialogue.npc2_talk();
        if (scanObject.name == "frontdesk") list_Dialogue.desk_portraitTest();
    }


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

    void PlayerHit()
    {
        anim.SetBool("isDamaged", true);
    }



    IEnumerator hit_Delay()
    {
        PlayerHit();

        
        yield return new WaitForSeconds(0.55f);
        anim.SetBool("isDamaged", false);
    }

}
