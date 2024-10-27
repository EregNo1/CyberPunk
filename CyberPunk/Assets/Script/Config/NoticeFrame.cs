using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeFrame : MonoBehaviour
{
    public Animator animator;
    public Animator popupBack;

    public Text txt_Detail;

    public Image btn_yes;
    public Image btn_no;

    public Sprite yes_On;
    public Sprite no_On;
    public Sprite yes_Off;
    public Sprite no_Off;

    [TextArea]
    public string toMain;

    [TextArea]
    public string exit;

    public bool isyes = true;
    public bool isendAni = false;


    public void notice_Open()
    {
        animator.Play("popup_ap");
        isyes = true;
    }
    public void notice_Close()
    {
        animator.Play("popup_dis");
        popupBack.Play("popupBack_close");
        isendAni = false;
    }

    public void noticeUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isyes = true;
            btn_yes.sprite = yes_On;
            btn_no.sprite = no_Off;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isyes = false;
            btn_yes.sprite = yes_Off;
            btn_no.sprite = no_On;
        }
    }


    public void setFalse()
    {
        gameObject.SetActive(false);
    }
    public void isEndAni()
    {
        isendAni = true;
    }


    public void setText_toMain()
    {
        txt_Detail.text = toMain;
    }

    public void setText_Exit()
    {
        txt_Detail.text = exit;
    }
}
