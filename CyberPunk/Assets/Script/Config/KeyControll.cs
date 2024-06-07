using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyControll : MonoBehaviour
{
    public Text txt_KeySwap;

    public Text txt_KeySwapS;
    public Image img_KeySwap_arrowL;
    public Image img_KeySwap_arrowR;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ConfigManager.configNum == 3)
        {
            txt_KeySwap.color = Color.black;
            txt_KeySwapS.color = Color.black;
            img_KeySwap_arrowL.color = Color.black;
            img_KeySwap_arrowR.color = Color.black;



        }
        else
        {
            {
                txt_KeySwap.color = Color.white;
                txt_KeySwapS.color = Color.white;
                img_KeySwap_arrowL.color = Color.white;
                img_KeySwap_arrowR.color = Color.white;

            }
        }
    }
}
