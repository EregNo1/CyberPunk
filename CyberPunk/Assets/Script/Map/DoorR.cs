using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorR : MonoBehaviour
{
    public Animator door_R;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        door_R.Play("doorR_Open");

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        door_R.Play("doorR_Close");
    }
}
