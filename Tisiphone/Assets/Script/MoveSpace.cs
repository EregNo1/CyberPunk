using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MoveSpace : MonoBehaviour
{
    public int spaceNum = 1;

    public GameObject space1;
    public GameObject space23;
    public GameObject space4;
    public GameObject space56;

    public GameObject transition;
    public Animator transition_animator;

    public Animator space23_animator;
    public Animator space56_animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveL()
    {
        //Debug.Log(spaceNum);

        if (spaceNum == 1) spaceNum = 6;
        else spaceNum--;

        if (spaceNum == 2 || spaceNum == 5)
        {
            StartCoroutine(spaceMovingL());
        }
        else StartCoroutine(moving());


    }
    public void moveR()
    {
        //Debug.Log(spaceNum);

        if (spaceNum == 6) spaceNum = 1;
        else spaceNum++;

        if (spaceNum == 3 || spaceNum == 6)
        {
            StartCoroutine(spaceMovingR());
        }
        else StartCoroutine(moving());

    }

    public void move()
    {
        if (spaceNum == 1)
        {
            space1.SetActive(true);
            space23.SetActive(false);
            space56.SetActive(false);
        }
        else if (spaceNum == 2)
        {
            space1.SetActive(false);
            space23.SetActive(true);
            space4.SetActive(false);
        }
        else if (spaceNum == 3)
        {
            space1.SetActive(false);
            space23.SetActive(true);
            space4.SetActive(false);
        }
        else if (spaceNum == 4)
        {
            space23.SetActive(false);
            space4.SetActive(true);
            space56.SetActive(false);
        }
        else if (spaceNum == 5)
        {
            space1.SetActive(false);
            space4.SetActive(false);
            space56.SetActive(true);
        }
        else if (spaceNum == 6)
        {
            space1.SetActive(false);
            space4.SetActive(false);
            space56.SetActive(true);
        }
    }

    IEnumerator spaceMovingL()
    {
        yield return new WaitForSeconds(0.2f);
        if (spaceNum == 2) space23_animator.Play("game_space3to2");
        if (spaceNum == 5) space56_animator.Play("game_space6to5");
    }

    IEnumerator spaceMovingR()
    {
        yield return new WaitForSeconds(0.2f);
        if (spaceNum == 3) space23_animator.Play("game_space2to3");
        if (spaceNum == 6) space56_animator.Play("game_space5to6");
    }

    IEnumerator moving()
    {

        transition.SetActive(true);
        transition_animator.Play("SpaceMove_transition");

        yield return new WaitForSeconds(0.35f);

        move();

        yield return new WaitForSeconds(0.4f);

        transition.SetActive(false);
    }
}
