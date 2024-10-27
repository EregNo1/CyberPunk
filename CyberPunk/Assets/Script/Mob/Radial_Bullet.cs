using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Radial_Bullet : MonoBehaviour
{

    Rigidbody2D radialBullet_rb;
    Animator animator;

    private void Start()
    {
        radialBullet_rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("벽에 충돌!");
            radialBullet_rb.velocity = Vector2.zero;
            animator.Play("Radial_Bullet_dis");
            StartCoroutine(BulletDestroy());

        }
    }


    //파괴 애니메이션 딜레이 코루틴
    IEnumerator BulletDestroy()
    {
        //animator.Play("mob1_BulletHit"); //총알 파괴 애니메이션 재생

        yield return new WaitForSeconds(0.5f);//파괴 애니메이션 딜레이

        Destroy(gameObject);
    }
}
