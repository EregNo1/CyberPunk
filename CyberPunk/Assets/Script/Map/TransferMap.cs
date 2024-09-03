using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMap : MonoBehaviour
{

    public Transform target;
    public GameObject transition_Canvas;
    public Animator transition;


    public GameObject playerCamera;
    GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(transfer());
        }
    }


    IEnumerator transfer()
    {
        transition_Canvas.SetActive(true);
        transition.Play("transition_on");
        PlayerController.ishit = true;
        PlayerController.rigid.velocity = Vector2.zero; 
        yield return new WaitForSeconds(0.75f);
        playerCamera.transform.position = target.position;
        player.transform.position = target.position;
        transition.Play("transition_off");
        yield return new WaitForSeconds(0.75f);
        transition_Canvas.SetActive(false);
        PlayerController.ishit = false;

    }

}
