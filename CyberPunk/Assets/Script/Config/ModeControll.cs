using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeControll : MonoBehaviour
{
    public Text txt_Mode;

    public Text txt_ModeS;
    public Image img_Mode_arrowL;
    public Image img_Mode_arrowR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ConfigManager.configNum == 2)
        {
            txt_Mode.color = Color.black;
            txt_ModeS.color = Color.black;
            img_Mode_arrowL.color = Color.black;
            img_Mode_arrowR.color = Color.black;



        }
        else
        {
            {
                txt_Mode.color = Color.white;
                txt_ModeS.color = Color.white;
                img_Mode_arrowL.color = Color.white;
                img_Mode_arrowR.color = Color.white;

            }
        }
    }
}
