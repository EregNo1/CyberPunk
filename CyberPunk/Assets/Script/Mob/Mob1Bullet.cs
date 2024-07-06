using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob1Bullet : MonoBehaviour
{
    [SerializeField] private float mob1_firePower = 15f; // �߻� ��
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
            Debug.Log("���� �浹!");
            mob1_bulletrb.velocity = Vector2.zero;
            animator.Play("mob1_BulletHit");
            StartCoroutine(Mob1BulletDestroy());

        }
        else if (collision.gameObject.CompareTag("Player"))
        {

            Debug.Log("�÷��̾�� �浹!");
            mob1_bulletrb.velocity = Vector2.zero; //�Ѿ� ����
            
            HP.hpNum--; //ü�� ����
            StartCoroutine(Mob1BulletDestroy()); //������ �ڷ�ƾ ���
            
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

    //�ı� �ִϸ��̼� ������ �ڷ�ƾ
    IEnumerator Mob1BulletDestroy()
    {
        animator.Play("mob1_BulletHit"); //�Ѿ� �ı� �ִϸ��̼� ���

        yield return new WaitForSeconds(0.5f);//�ı� �ִϸ��̼� ������

        Destroy(gameObject);
    }
}
