using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseFrame : MonoBehaviour
{
    public Animator animator;
    public Animator popupBack;

    public int menuNum = 0;

    // Start is called before the first frame update



    public void pause_Open()
    {
        menuNum = 0;
        popupBack.Play("popupBack_open");
        animator.Play("menu_ap");
    }
    public void pause_Close()
    {
        animator.SetTrigger("isClose");
        //popupBack.Play("popupBack_close");
    }
    public void setFalse()
    {
        gameObject.SetActive(false);
    }


    public void menuUpdate()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(menuNum < 2)
            {
                menuNum++;
                animator.SetInteger("menuNum", menuNum);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(menuNum > 0)
            {
                menuNum--;
                animator.SetInteger("menuNum", menuNum);
            }
        }

    }



}
