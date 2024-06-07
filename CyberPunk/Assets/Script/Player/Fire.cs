using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;


    [SerializeField] private float fireDelay = 0.1f; // 발사 딜레이
    private float lastFireTime = 0; // 마지막 발사 시간

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Bullet.direction = Vector2.up;
            fireBullet();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Bullet.direction = Vector2.left;
            fireBullet();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Bullet.direction = Vector2.down;
            fireBullet();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Bullet.direction = Vector2.right;
            fireBullet();
        }
    }

    public void fireBullet()
    {
        if (Time.time - lastFireTime >= fireDelay)
        {
            Instantiate(bulletPref, transform.position, transform.rotation);

            lastFireTime = Time.time;
        }

    }


}
