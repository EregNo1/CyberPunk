using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flooring : MonoBehaviour
{
    public MonsterStats stats;
    public GameObject markPrf;
    float currentHealth;

    public float markSpawn = 1f;

    Rigidbody2D flooring_rb;
    Vector2 flooring_movement;
    //SpriteRenderer slime_sprite;



    // Start is called before the first frame update
    void Start()
    {
        flooring_rb = GetComponent<Rigidbody2D>();
        SetRandomDirection();
        StartCoroutine(SpawnMark());
        currentHealth = stats.health;

        //StartCoroutine(Flooring_AI());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, flooring_movement, 1f, LayerMask.GetMask("Border"));
        if (hit.collider != null)
        {
            SetRandomDirection();
        }

        flooring_rb.velocity = flooring_movement * stats.movementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bujeok"))
        {
            TakeDamage(PlayerStats.Instance.attackDamage);
        }
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
        RoomManager.instance.monsterDefeated();
        Destroy(gameObject);
    }

    void SetRandomDirection()
    {
        int randomDir = Random.Range(0, 4);
        switch (randomDir)
        {
            case 0:
                flooring_movement = Vector2.up;
                break;
            case 1:
                flooring_movement = Vector2.down;
                break;
            case 2:
                flooring_movement = Vector2.left;
                break;
            case 3:
                flooring_movement = Vector2.right;
                break;
        }
    }

    /*IEnumerator Flooring_AI()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            SetRandomDirection();
        }
    }*/

    IEnumerator SpawnMark()
    {
        while (true)
        {
            yield return new WaitForSeconds(markSpawn);
            Instantiate(markPrf, transform.position, Quaternion.identity);
        }
    }
}
