using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobyGate : MonoBehaviour
{
    public GameObject gateBlcckL;
    public Animator gateL_L;
    public Animator gateL_R;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        gateL_L.Play("LobyDoor_L");
        gateL_R.Play("LobyDoor_R");
        gateBlcckL.SetActive(false);
        gameObject.SetActive(false);
        //L����Ʈ �ִϸ��̼� ���
        //L ��� ����
        //Ʈ���� ����



    }
}
