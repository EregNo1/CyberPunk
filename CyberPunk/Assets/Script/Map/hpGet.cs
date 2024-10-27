using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpGet : MonoBehaviour
{
    public HP hpManager;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    //player 콜라이더와 닿으면 체력 +1 후 소멸. 이미 만땅일때에는 작동X

    //

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (hpManager.hpNum < 3)
            {
                animator.Play("hpItem_dis");
                hpManager.hpNum++;
                hpManager.hpUpdate();
            }
            else return;

        }
    }

    public void destroyItem()
    {
        Destroy(gameObject);
    }

    public void idleAni()
    {
        animator.Play("hpItem_Idle");
    }
}
