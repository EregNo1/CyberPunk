using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class Radial_Bullet : MonoBehaviour
{

    Rigidbody2D radialBullet_rb;


    private void Start()
    {
        radialBullet_rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            Debug.Log("���� �浹!");
            radialBullet_rb.velocity = Vector2.zero;
            //animator.Play("mob1_BulletHit");
            StartCoroutine(BulletDestroy());

        }
    }


    //�ı� �ִϸ��̼� ������ �ڷ�ƾ
    IEnumerator BulletDestroy()
    {
        //animator.Play("mob1_BulletHit"); //�Ѿ� �ı� �ִϸ��̼� ���

        yield return new WaitForSeconds(0.5f);//�ı� �ִϸ��̼� ������

        Destroy(gameObject);
    }
}