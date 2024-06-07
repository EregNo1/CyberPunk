using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 10f;


    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(moveInput * speed * Time.deltaTime, 0, 0);
    }
}
