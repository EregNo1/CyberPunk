using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXControll : MonoBehaviour
{
    public Text txt_SFX;

    [Header("이미지 오브젝트")]
    public Image[] img_SFX;


    [Header("변경용 이미지")]
    public Sprite setting_gageL_blank;
    public Sprite setting_gageL_fill;
    public Sprite setting_gageM_blank;
    public Sprite setting_gageM_fill;
    public Sprite setting_gageR_blank;
    public Sprite setting_gageR_fill;

    public int sfxNum = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ConfigManager.configNum == 1)
        {
            txt_SFX.color = Color.black;

            for (int i = 0; i > 11; i++)
            {
                img_SFX[i].color = Color.black;
            }

        }
        else
        {
            {
                txt_SFX.color = Color.white;

                for (int i = 0; i > 11; i++)
                {
                    img_SFX[i].color = Color.white;
                }
            }
        }
    }
}
