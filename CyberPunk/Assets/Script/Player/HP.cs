using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public Image hpFrame;

    public Sprite hp_3;
    public Sprite hp_2;
    public Sprite hp_1;
    public Sprite hp_0;

    public static int hpNum = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hpNum);

        if (hpNum == 3)
        {
            Debug.Log(hpNum);
            hpFrame.sprite = hp_3;
        }

        if (hpNum == 2)
        {
            Debug.Log(hpNum);
            hpFrame.sprite = hp_2;
        }

        if (hpNum == 1)
        {
            Debug.Log(hpNum);
            hpFrame.sprite = hp_1;
        }

        if (hpNum == 0)
        {
            Debug.Log(hpNum);
            hpFrame.sprite = hp_0;
        }
    }
}
