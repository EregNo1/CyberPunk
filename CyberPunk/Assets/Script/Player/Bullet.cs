using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] private GameObject RangeHitEffect; // ��Ʈ ȿ�� ������

    // Start is called before the first frame update

    [SerializeField] private float firePower = 15f; // �߻� ��
    public static Vector2 direction;


    void Start()
    {
        Invoke("DestroyTime", 2.0f);

        GetComponent<Rigidbody2D>().AddForce(direction * firePower, ForceMode2D.Impulse);

    }

    void DestroyTime()
    {
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("���� �浹!");
            //Instantiate(RangeHitEffect, transform.position, Quaternion.identity); // ��Ʈ ȿ�� ����
            Destroy(gameObject);
        }
        /*else if (collision.gameObject.CompareTag("Monster"))
        {
            Debug.Log("���Ϳ��� �浹!");
            //Instantiate(RangeHitEffect, transform.position, Quaternion.identity); // ��Ʈ ȿ�� ����
            // collision.SendMessage("Demaged", 10); // Demaged �Լ� ȣ��, ���Ÿ� ���ݷ�(10, �ӽ�)��ŭ ����  TODO
            Destroy(gameObject);
        }*/
    }
}

