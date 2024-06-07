using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dial1_ani : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMouseEnter()
    {
        animator.SetBool("ishover", true);
    }

    public void OnMouseExit()
    {
        animator.SetBool("ishover", false);
    }
}
