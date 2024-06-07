using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





public class VolumeControll : MonoBehaviour
{
    public Text txt_BGM;

    [Header("이미지 오브젝트")]
    public Image[] img_BGM;


    [Header("변경용 이미지")]
    public Sprite setting_gageL_blank;
    public Sprite setting_gageL_fill;
    public Sprite setting_gageM_blank;
    public Sprite setting_gageM_fill;
    public Sprite setting_gageR_blank;
    public Sprite setting_gageR_fill;

    public int bgmNum = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ConfigManager.configNum == 0)
        {
            txt_BGM.color = Color.black;

            for (int i = 0; i > 11; i++)
            {
                img_BGM[i].color = Color.black;
            }

        }
        else
        {
            {
                txt_BGM.color = Color.white;

                for (int i = 0; i > 11; i++)
                {
                    img_BGM[i].color = Color.white;
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bgmNum--;
            volumeChange();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            bgmNum++;
            volumeChange();
        }



    }

    public void volumeChange()
    {
       //bgmNum = arrayNum;


        if (bgmNum == 0) img_BGM[0].sprite = setting_gageL_blank;
        if (bgmNum == 1) img_BGM[1].sprite = setting_gageL_blank;
        if (bgmNum == 2) img_BGM[2].sprite = setting_gageM_blank;
        if (bgmNum == 3) img_BGM[3].sprite = setting_gageM_blank;
        if (bgmNum == 4) img_BGM[4].sprite = setting_gageM_blank;
        if (bgmNum == 5) img_BGM[5].sprite = setting_gageM_blank;
        if (bgmNum == 6) img_BGM[6].sprite = setting_gageM_blank;
        if (bgmNum == 7) img_BGM[7].sprite = setting_gageM_blank;
        if (bgmNum == 8) img_BGM[8].sprite = setting_gageM_blank;
        if (bgmNum == 9) img_BGM[9].sprite = setting_gageR_blank;
        if (bgmNum == 10) img_BGM[10].sprite = setting_gageR_fill;
        
    }
}
