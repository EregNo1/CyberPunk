using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Image hpFrame;
    public Animator uiFrame;
    public Animator vignetteColor;
    
    public Sprite hp_3;
    public Sprite hp_2;
    public Sprite hp_1;
    public Sprite hp_0;

    public int hpNum = 3;


    public void damage_Ani()
    {

        StartCoroutine(damage());

    }

    public void hpUpdate()
    {
        Debug.Log(hpNum);

        if (hpNum == 3)
        {
            Debug.Log(hpNum);
            hpFrame.sprite = hp_3;
        }

        else if (hpNum == 2)
        {
            Debug.Log(hpNum);
            hpFrame.sprite = hp_2;
        }

        else if (hpNum == 1)
        {
            Debug.Log(hpNum);
            hpFrame.sprite = hp_1;
        }

        else if (hpNum == 0)
        {
            Debug.Log(hpNum);
            hpFrame.sprite = hp_0;
        }
    }

    IEnumerator damage()
    {
        vignetteColor.SetBool("isDamaged", true);
        uiFrame.SetBool("isDamaged", true);


        yield return new WaitForSeconds(1f);

        uiFrame.SetBool("isDamaged", false);
        vignetteColor.SetBool("isDamaged", false);
    }
}
