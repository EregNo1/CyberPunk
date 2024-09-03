using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MiddleBoss : MonoBehaviour
{
    //���� ����
    float currentHealth = 120;//���� ü��
    public GameObject middleBoss_BulletPref; //�߰����� ź�� ������
    public Transform middleBoss_fireSpot; //�߰����� ź�� �߻� ��ġ
    public float middleBoss_Speed; //�̵��ӵ�
    Rigidbody2D middleBoss_rb;
    Transform player; //�÷��̾� ��ġ
    Vector2 middleBoss_Movement;
    bool isSkill = false;

    //����1 ����
    public float pattern1_shootDelay; //���� �ӵ�
    public float pattern1_shootNum; //���� ����
    public float pattern1_bulletSpeed; //ź�� �ӵ�
    public int pattern1_numberOfBullet; // �ѹ��� �߻��ϴ� ź�� ����

    //����2 ����
    public float pattern2_shootDelay; //���� �ӵ�
    public float pattern2_shootNum; //���� ����
    public float pattern2_bulletSpeed; //ź�� �ӵ�
    Vector2 pattern2_fireDirection;

    //����3 ����
    public GameObject slimePrf;
    public Transform[] spawnPoints;

    //����4 ����
    public GameObject palpPrf; //�˼� ������
    public float pattern4_readyDelay; //�߻� �� ������
    public float pattern4_shootDelay; //���� �ӵ�
    public float pattern4_Delay; //�߻� �� ������
    public float pattern4_shootNum; //���� Ƚ��
    public int pattern4_PalpNum; //�ѹ��� ��ȯ�Ǵ� �˼� ����
    public Transform[] palpFirePoint; //�߻� ������
    public Transform[] palpFirePoint2; //�߻� ������ �ٸ� ������� �õ�

    //����5 ����
    float healPoint;
    bool alreadyHeal = false;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //�÷��̾� ������Ʈ Ž��
        middleBoss_rb = GetComponent<Rigidbody2D>();
        healPoint = currentHealth / 3;
        StartCoroutine(Play_MiddleBoss());
    }

    private void Update()
    {
        if(player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            middleBoss_Movement = direction;
        }
    }

    private void FixedUpdate()
    {
        if (!isSkill)
            MiddleBoss_Move(middleBoss_Movement);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bujeok"))
        {
            TakeDamage(PlayerStats.Instance.attackDamage);
            middleBoss_Pattern5();
            Debug.Log(currentHealth);
        }
    }

    void MiddleBoss_Move(Vector2 dir)
    {
        middleBoss_rb.velocity = dir * middleBoss_Speed;
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
    }



    //����1 : 16������ ź�� �߻� 5��
    public void middleBoss_Pattern1()
    {
        float angleStep = 360f / pattern1_numberOfBullet;

        for (int i = 0; i < pattern1_numberOfBullet; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            Vector2 bulletDirection = rotation * Vector2.right;

            GameObject bullet = Instantiate(middleBoss_BulletPref, middleBoss_fireSpot.position, rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            bulletRb.velocity = bulletDirection.normalized * pattern1_bulletSpeed;
        }
    }
    IEnumerator Pattern1_shoot()
    {
        isSkill = true;
        middleBoss_rb.velocity = Vector2.zero;

        for (int i = 0; i < pattern1_shootNum; i++)
        {
            middleBoss_Pattern1();
            yield return new WaitForSeconds(pattern1_shootDelay);
        }
        yield return new WaitForSeconds(0.1f);
        isSkill = false;

    }

    public void Play_Pattern1() //����1 ���
    {
        StartCoroutine(Pattern1_shoot());
    }


    //����2 : �÷��̾ ���� ź�� 10�� ���� �߻�
    public void middleBoss_Patter2()
    {
        pattern2_fireDirection = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(pattern2_fireDirection.y, pattern2_fireDirection.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(middleBoss_BulletPref, middleBoss_fireSpot.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        bulletRb.velocity = pattern2_fireDirection.normalized * pattern2_bulletSpeed;
    }

    IEnumerator Pattern2_shoot()
    {
        isSkill = true;
        middleBoss_rb.velocity = Vector2.zero;
        for (int i = 0; i < pattern2_shootNum; i++)
        {
            middleBoss_Patter2();
            yield return new WaitForSeconds(pattern2_shootDelay);
        }
        yield return new WaitForSeconds(0.1f);
        isSkill = false;
    }
    public void Play_pattern2()
    {
        StartCoroutine (Pattern2_shoot());
    }

    //����3 : ������ 3���� ��ȯ. �ֱ������� �ߵ�
    public void middleBoss_Pattern3()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(slimePrf, spawnPoints[i].position, Quaternion.identity);
        }
    }

    public void Play_pattern3()
    {
        middleBoss_Pattern3();
    }

    //����4 : ���� ���� ��ġ�� �˼�(������) �߻�. �ߵ� �߿� ���� 1,2,3�� �ߵ����� ����.

    public void middleBoss_Pattern4()
    {
        List<int> indices = new List<int>();

        for (int i = 0; i < palpFirePoint.Length; i++)
        {
            indices.Add(i);
        }


        List<int> selectIndices = new List<int>();

        for (int i = 0; i < pattern4_PalpNum; i++)
        {
            int randomIndex = Random.Range(0, indices.Count);
            selectIndices.Add(indices[randomIndex]);
            indices.RemoveAt(randomIndex);
        }

        foreach (int index in selectIndices)
        {
            //Instantiate(palpPrf, palpFirePoint[index].position, Quaternion.identity);
            StartCoroutine(pattern4_Warning(index));

        }

    }

    IEnumerator pattern4_Warning(int idx)
    {
        //��� Ȱ��ȭ
        palpFirePoint2[idx].transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(pattern4_readyDelay); //��� ���� �ð�
        //�˼� Ȱ��ȭ
        palpFirePoint2[idx].transform.GetChild(0).gameObject.SetActive(false);
        palpFirePoint2[idx].transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(pattern4_shootDelay); //�˼� ���� �ð�
        //�˼� ��Ȱ��ȭ
        palpFirePoint2[idx].transform.GetChild(1).gameObject.SetActive(false);
    }

    IEnumerator Pattern4_shoot()
    {
        isSkill = true;
        middleBoss_rb.velocity = Vector2.zero;
        for (int i = 0; i < pattern4_shootNum; i++)
        {
            middleBoss_Pattern4();
            yield return new WaitForSeconds(pattern4_Delay); //���� Ȱ��ȭ ������ ���
        }
        yield return new WaitForSeconds(0.1f);
        isSkill = false;
    }

    //pattern4 ��� �ð����� �ʵ忡�� ������ٰ� ���� �� �ٽ� ����


    public void Play_Pattern4()
    {
        StartCoroutine(Pattern4_shoot());
    }


    //����5 : ü���� 3���� 1 ���Ϸ� �������� �� �ѹ� 3���� 2�������� ȸ��.
    public void middleBoss_Pattern5()
    {
        if(alreadyHeal == false)
        {
            if (currentHealth <= healPoint)
            {
                currentHealth = currentHealth + healPoint;
                alreadyHeal = true;
            }
        }

    }



    IEnumerator Play_MiddleBoss()
    {
        yield return new WaitForSeconds(3f); //���� �� ������

        Play_Pattern1();
        yield return new WaitForSeconds(5f);
        Play_pattern2();
        yield return new WaitForSeconds(5f);
        Play_Pattern1();
        yield return new WaitForSeconds(5f);
        Play_Pattern1();
        Play_pattern3();
        yield return new WaitForSeconds(5f);
        Play_pattern2();
        yield return new WaitForSeconds(3f);
        Play_pattern3();
        yield return new WaitForSeconds(5f);
        Play_Pattern1();
        Play_pattern2();
        yield return new WaitForSeconds(3f);
        Play_Pattern4();
        yield return new WaitForSeconds(10f);
        StartCoroutine(Play_MiddleBoss());
    }

}
