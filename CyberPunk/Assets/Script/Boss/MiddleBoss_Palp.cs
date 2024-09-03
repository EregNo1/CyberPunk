using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleBoss_Palp : MonoBehaviour
{
    public float expandSpeed = 50f;
    public float duration = 3f;

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
            //�߻� �غ� �� 
            transform.localScale += new Vector3(0, expandSpeed * Time.deltaTime, 0);
            transform.position = new Vector3(initialPosition.x , initialPosition.y + (transform.localScale.y / 2), initialPosition.z);
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
        Debug.Log("������ �߻�!");
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
        Debug.Log("�ı�");
    }
}
