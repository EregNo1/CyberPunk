using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorL : MonoBehaviour
{
    public Animator door_L;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        door_L.Play("doorL_Open");

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        door_L.Play("doorL_Close");
    }
}
