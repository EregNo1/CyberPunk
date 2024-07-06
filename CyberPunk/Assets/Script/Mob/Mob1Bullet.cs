using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1Bullet : MonoBehaviour
{
    [SerializeField] private float mob1_firePower = 15f; // 발사 힘
    public static Vector2 mob1_direction;
    public Transform mob1_rotate;
    Animator animator;
    Rigidbody2D mob1_bulletrb;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyTime", 2.0f);
        StartCoroutine(Mob1BulletStart());
        mob1_bulletrb = GetComponent<Rigidbody2D>();
        mob1_direction = transform.right;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("벽에 충돌!");
            mob1_bulletrb.velocity = Vector2.zero;
            animator.Play("mob1_BulletHit");
            StartCoroutine(Mob1BulletDestroy());

        }
        else if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("플레이어에게 충돌!");
            mob1_bulletrb.velocity = Vector2.zero; //총알 정지
            
            HP.hpNum--; //체력 감소
            StartCoroutine(Mob1BulletDestroy()); //딜레이 코루틴 재생
            
        }
    }

    public static Vector2 GetBulletDirection()
    {
        return mob1_direction;
    }

    IEnumerator Mob1BulletStart()
    {
        yield return new WaitForSeconds(0.6f);

        mob1_bulletrb.velocity = mob1_direction * mob1_firePower;
    }

    //파괴 애니메이션 딜레이 코루틴
    IEnumerator Mob1BulletDestroy()
    {
        animator.Play("mob1_BulletHit"); //총알 파괴 애니메이션 재생

        yield return new WaitForSeconds(0.5f);//파괴 애니메이션 딜레이

        Destroy(gameObject);
    }
}
