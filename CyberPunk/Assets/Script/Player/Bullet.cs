using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private GameObject RangeHitEffect; // ��Ʈ ȿ�� ������

    // Start is called before the first frame update

    [SerializeField] private float firePower = 15f; // �߻� ��
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




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("���� �浹!");
            bulletrb.velocity = Vector2.zero;
            animator.Play("bujeok_hit");
            StartCoroutine(DestroyBujeok());

        }
        else if (collision.gameObject.CompareTag("Mob"))
        {
            Debug.Log("���Ϳ��� �浹!");
            //Instantiate(RangeHitEffect, transform.position, Quaternion.identity); // ��Ʈ ȿ�� ����
            // collision.SendMessage("Demaged", 10); // Demaged �Լ� ȣ��, ���Ÿ� ���ݷ�(10, �ӽ�)��ŭ ����  TODO
            StartCoroutine(DestroyBujeok());
        }
    }


    IEnumerator DestroyBujeok()
    {
        //animator.SetTrigger("isHit");

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

}

