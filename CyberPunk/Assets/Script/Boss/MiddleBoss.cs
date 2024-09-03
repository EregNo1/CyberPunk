using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MiddleBoss : MonoBehaviour
{
    //몬스터 정보
    float currentHealth = 120;//현재 체력
    public GameObject middleBoss_BulletPref; //중간보스 탄막 프리팹
    public Transform middleBoss_fireSpot; //중간보스 탄막 발사 위치
    public float middleBoss_Speed; //이동속도
    Rigidbody2D middleBoss_rb;
    Transform player; //플레이어 위치
    Vector2 middleBoss_Movement;
    bool isSkill = false;

    //패턴1 관련
    public float pattern1_shootDelay; //연발 속도
    public float pattern1_shootNum; //연발 개수
    public float pattern1_bulletSpeed; //탄막 속도
    public int pattern1_numberOfBullet; // 한번에 발사하는 탄막 개수

    //패턴2 관련
    public float pattern2_shootDelay; //연발 속도
    public float pattern2_shootNum; //연발 개수
    public float pattern2_bulletSpeed; //탄막 속도
    Vector2 pattern2_fireDirection;

    //패턴3 관련
    public GameObject slimePrf;
    public Transform[] spawnPoints;

    //패턴4 관련
    public GameObject palpPrf; //촉수 프리팹
    public float pattern4_readyDelay; //발사 전 딜레이
    public float pattern4_shootDelay; //연발 속도
    public float pattern4_Delay; //발사 후 딜레이
    public float pattern4_shootNum; //연발 횟수
    public int pattern4_PalpNum; //한번에 소환되는 촉수 개수
    public Transform[] palpFirePoint; //발사 시작점
    public Transform[] palpFirePoint2; //발사 시작점 다른 방법으로 시도

    //패턴5 관련
    float healPoint;
    bool alreadyHeal = false;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //플레이어 오브젝트 탐색
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



    //패턴1 : 16갈래로 탄막 발사 5발
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

    public void Play_Pattern1() //패턴1 재생
    {
        StartCoroutine(Pattern1_shoot());
    }


    //패턴2 : 플레이어를 항해 탄막 10발 연속 발사
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

    //패턴3 : 슬라임 3마리 소환. 주기적으로 발동
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

    //패턴4 : 세로 랜덤 위치에 촉수(혈사포) 발사. 발동 중엔 패턴 1,2,3이 발동하지 않음.

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
        //경고 활성화
        palpFirePoint2[idx].transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(pattern4_readyDelay); //경고 유지 시간
        //촉수 활성화
        palpFirePoint2[idx].transform.GetChild(0).gameObject.SetActive(false);
        palpFirePoint2[idx].transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(pattern4_shootDelay); //촉수 유지 시간
        //촉수 비활성화
        palpFirePoint2[idx].transform.GetChild(1).gameObject.SetActive(false);
    }

    IEnumerator Pattern4_shoot()
    {
        isSkill = true;
        middleBoss_rb.velocity = Vector2.zero;
        for (int i = 0; i < pattern4_shootNum; i++)
        {
            middleBoss_Pattern4();
            yield return new WaitForSeconds(pattern4_Delay); //다음 활성화 까지의 대기
        }
        yield return new WaitForSeconds(0.1f);
        isSkill = false;
    }

    //pattern4 재생 시간동안 필드에서 사라졌다가 종료 후 다시 등장


    public void Play_Pattern4()
    {
        StartCoroutine(Pattern4_shoot());
    }


    //패턴5 : 체력이 3분의 1 이하로 떨어지면 단 한번 3분의 2지점까지 회복.
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
        yield return new WaitForSeconds(3f); //시작 전 딜레이

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
