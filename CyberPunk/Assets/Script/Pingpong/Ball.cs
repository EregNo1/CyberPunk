using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed;
    public static int hitnum = 0;

    void Start()
    {
        Init();
        
    }

    void Update()
    {
        if(hitnum == 5)
        {
            Time.timeScale = 0;
        }
        
    }

    void Init()
    {
        transform.position = new Vector3(0, -2, 0);
        float randomDirection = Random.Range(0, 2) * 2 - 1; // -1 or 1
        GetComponent<Rigidbody2D>().velocity = new Vector2(randomDirection, 1).normalized * ballSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
            /*float sx = Random.Range(0, 2) == 0 ? -1 : 1;
            float sy = Random.Range(0, 2) == 0 ? -1 : 1;
            GetComponent<Rigidbody2D>().velocity = new Vector3(ballSpeed * sx, ballSpeed * sy, 0);
            Debug.Log("Hit");*/
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall_over"))
        {
            Debug.Log("Game Over");
            gameObject.SetActive(false);
        }
    }

}
