using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Bullet : MonoBehaviour
{

    SpriteRenderer sr;
    BoxCollider2D bc;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, LayerMask.GetMask("Border"));
        sr.size = new Vector2(ray.distance, 0.85f);
        bc.size = new Vector2(ray.distance, 0.85f);
        bc.offset = new Vector2(ray.distance / 2, 0);
    }
}
