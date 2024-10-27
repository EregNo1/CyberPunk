using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Bullet : MonoBehaviour
{
    Transform player;

    public float bulletSpeed = 5f;
    public float delayBeforeFire = 0.5f;

    Animator animator;
    Rigidbody2D ghostBullet_rb;
    Vector2 fireDirection;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ghostBullet_rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        StartCoroutine(BulletFire());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("벽에 충돌!");
            ghostBullet_rb.velocity = Vector2.zero;
            //animator.Play("mob1_BulletHit");
            StartCoroutine(Mob1BulletDestroy());

        }
    }

    void SetBulletDirection()
    {
        fireDirection = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    IEnumerator BulletFire()
    {
        yield return new WaitForSeconds(delayBeforeFire);
        SetBulletDirection();
        ghostBullet_rb.velocity = fireDirection * bulletSpeed;
    }

    //파괴 애니메이션 딜레이 코루틴
    IEnumerator Mob1BulletDestroy()
    {
        animator.Play("mob1_BulletHit"); //총알 파괴 애니메이션 재생

        yield return new WaitForSeconds(0.5f);//파괴 애니메이션 딜레이

        Destroy(gameObject);
    }
}
