using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenAni : MonoBehaviour
{
    public GameObject visible;
    public GameObject visible_background;
    public Animator hand;
    public Animator background;
    public GameObject btn_InvenX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playHand()
    {
        
        visible.SetActive(true);
        hand.Play("Inven_hand");
    }

    public void playBackground()
    {
        visible_background.SetActive(true);
        background.Play("Inven_background");
    }

    public void offHand()
    {
        btn_InvenX.SetActive(false);
        hand.Play("Inven_handOff");
        
    }
    public void offBackground()
    {
        background.Play("Inven_backgroundOff");
    }

    public void btnX()
    {
        btn_InvenX.SetActive(true);
    }
    public void invisible()
    {
        visible.SetActive(false);
        visible_background.SetActive(false);
        btn_InvenX.SetActive(false);
    }
}
