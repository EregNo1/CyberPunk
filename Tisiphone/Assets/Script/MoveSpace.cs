using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static System.TimeZoneInfo;

public class MoveSpace : MonoBehaviour
{

    public GameObject[] spaces;
    public int spaceNum = 0;

    public RectTransform space2and3Rect;
    public RectTransform space5and6Rect;
    public float animationDuration = 0.5f;

    bool isSpace2Next = false;
    bool isSpace2Prev = false;
    bool isSpace5Next = false;
    bool isSpace5Prev = false;



    //애니메이션
    bool isAnimate = false;
    public Animator transition;
    public GameObject transition_img;

    public Animator room23_transition;
    public Animator room56_transition;


    // Start is called before the first frame update
    void Start()
    {
        updateSpaces();
    }

    // Update is called once per frame


    void updateSpaces()
    {
        for (int i = 0; i < spaces.Length; i++)
        {
            spaces[i].SetActive(i == spaceNum);
        }
    }


    public void moveNext()
    {
        if(isAnimate == false)
        {
            if (spaceNum == 1 && !isSpace2Next)
            {
                isSpace2Next = true;
                isSpace2Prev = false;
                Debug.Log("Space3");

                StartCoroutine(room2to3_slideTransition());
            }
            else if (spaceNum == 3 && !isSpace5Next)
            {
                isSpace5Next = true;
                isSpace5Prev = false;
                Debug.Log("Space6");
                StartCoroutine(room5to6_slideTransition());
            }
            else
            {
                StartCoroutine(spaceMoveTransition());

                spaceNum = (spaceNum + 1) % spaces.Length;
                Resetflags();
                
            }
        }
    }

    public void movePrevious()
    {
        if(isAnimate == false)
        {
            if (spaceNum == 1 && !isSpace2Prev)
            {
                isSpace2Prev = true;
                isSpace2Next = false;
                Debug.Log("Space2");
                StartCoroutine(room3to2_slideTransition());
            }
            else if (spaceNum == 3 && !isSpace5Prev)
            {
                isSpace5Prev = true;
                isSpace5Next = false;
                Debug.Log("Space5");
                StartCoroutine(room6to5_slideTransition());
            }
            else
            {
                StartCoroutine(spaceMoveTransition());

                spaceNum = (spaceNum - 1 + spaces.Length) % spaces.Length;
                Resetflags();
                
            }

        }
    }


    void Resetflags()
    {
        if(spaceNum != 1 || spaceNum != 3)
        {
            isSpace2Next = false;
            isSpace2Prev = false;
            isSpace5Next = false;
            isSpace5Prev = false;

        }
    }


    IEnumerator spaceMoveTransition()
    {
        isAnimate = true;
        transition_img.SetActive(true);

        transition.Play("SpaceMove_transition");
        yield return new WaitForSeconds(0.35f);
        updateSpaces();
        yield return new WaitForSeconds(0.35f);

        transition_img.SetActive(false);
        isAnimate = false;
    }

    IEnumerator room2to3_slideTransition()
    {
        isAnimate = true;

        room23_transition.Play("game_space2to3");
        yield return new WaitForSeconds(0.5f);

        isAnimate = false;
    }
    IEnumerator room3to2_slideTransition()
    {
        isAnimate = true;

        room23_transition.Play("game_space3to2");
        yield return new WaitForSeconds(0.5f);

        isAnimate = false;
    }
    IEnumerator room5to6_slideTransition()
    {
        isAnimate = true;

        room56_transition.Play("game_space5to6");
        yield return new WaitForSeconds(0.5f);

        isAnimate = false;
    }
    IEnumerator room6to5_slideTransition()
    {
        isAnimate = true;

        room56_transition.Play("game_space6to5");
        yield return new WaitForSeconds(0.5f);

        isAnimate = false;
    }





}
