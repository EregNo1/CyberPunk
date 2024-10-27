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
        if (PlayerController.keySwap == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                //Bullet.rotate.rotation = Quaternion.Euler(0, 0, 270);
                Bullet.rotation = 90f;
                Bullet.direction = Vector2.up;
                fireBullet();

            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                //Bullet.rotate.rotation = Quaternion.Euler(0, 0, 180);
                Bullet.rotation = 180f;
                Bullet.direction = Vector2.left;
                fireBullet();

            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                //Bullet.rotate.rotation = Quaternion.Euler(0, 0, 90);
                Bullet.rotation = 270f;
                Bullet.direction = Vector2.down;
                fireBullet();

            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                //Bullet.rotate.rotation = Quaternion.Euler(0, 0, 0);
                Bullet.rotation = 0f;
                Bullet.direction = Vector2.right;
                fireBullet();
            }
        }

        else if (PlayerController.keySwap == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Bullet.rotate.rotation = Quaternion.Euler(0, 0, 270);
                Bullet.rotation = 90f;
                Bullet.direction = Vector2.up;
                fireBullet();

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Bullet.rotate.rotation = Quaternion.Euler(0, 0, 180);
                Bullet.rotation = 180f;
                Bullet.direction = Vector2.left;
                fireBullet();

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Bullet.rotate.rotation = Quaternion.Euler(0, 0, 90);
                Bullet.rotation = 270f;
                Bullet.direction = Vector2.down;
                fireBullet();

            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Bullet.rotate.rotation = Quaternion.Euler(0, 0, 0);
                Bullet.rotation = 0f;
                Bullet.direction = Vector2.right;
                fireBullet();
            }
        }
    }

    public void fireBullet()
    {


        if (Time.time - lastFireTime >= fireDelay)
        {
            Instantiate(bulletPref, transform.position, Quaternion.identity);
            lastFireTime = Time.time;
        }

    }


}
