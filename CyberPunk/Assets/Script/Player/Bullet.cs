using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private GameObject RangeHitEffect; // È÷Æ® È¿°ú ÇÁ¸®ÆÕ

    // Start is called before the first frame update

    [SerializeField] private float firePower = 15f; // ¹ß»ç Èû
    public static Vector2 direction;
    public static float rotation;
    Animator animator;
    Rigidbody2D bulletrb;

    void Start()
    {
        Invoke("DestroyTime", 2.0f);

        bulletrb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        bulletrb.velocity = direction * firePower;

        animator = GetComponent<Animator>();



    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.Play("bujeok_hit");
        bulletrb.velocity = Vector2.zero;
        StartCoroutine(DestroyBujeok());
    }


    IEnumerator DestroyBujeok()
    {
        //animator.SetTrigger("isHit");

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

}

