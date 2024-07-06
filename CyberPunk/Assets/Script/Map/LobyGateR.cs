using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobyGateR : MonoBehaviour
{
    public GameObject gateBlcckR;
    public Animator gateR_L;
    public Animator gateR_R;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        gateR_L.Play("LobyDoor_L2");
        gateR_R.Play("LobyDoor_R2");
        gateBlcckR.SetActive(false);
        gameObject.SetActive(false);
        //L게이트 애니메이션 재생
        //L 블록 제거
        //트리거 제거



    }
}
