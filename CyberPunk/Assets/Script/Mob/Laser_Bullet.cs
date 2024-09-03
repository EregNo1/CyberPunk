using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Bullet : MonoBehaviour
{
    public float expandSpeed = 10f;
    public float duration = 4f;

    bool isExpanding = true;
    BoxCollider2D boxCol;
    Vector3 initialPosition;


    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        initialPosition = transform.position;
        StartCoroutine(LaserDuration());
    }

    // Update is called once per frame
    void Update()
    {
        if (isExpanding)
        {
            transform.localScale += new Vector3(expandSpeed * Time.deltaTime, 0, 0);
            transform.position = new Vector3(initialPosition.x + (transform.localScale.x / 1.2f), initialPosition.y, initialPosition.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            isExpanding = false;
        }
    }

    IEnumerator LaserDuration()
    {
        Debug.Log("레이저 발사!");
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
        Debug.Log("파괴");
    }
}
