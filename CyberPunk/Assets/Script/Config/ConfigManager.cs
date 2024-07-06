using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigManager : MonoBehaviour
{
    public Image btn_save;
    //public Image btn_default;
    //public Image btn_select;

    public static int configNum = 0;
    public RectTransform SettingAni;

    public float duration = 1f; // 애니메이션 지속 시간

    private Vector2 startPosition; // 시작 위치
    private float elapsedTime = 0f; // 경과 시간
    private bool isAnimating = false; // 애니메이션 상태

    public int[] targetHeight;
    int array = 0;



    void Start()
    {
        // 초기 위치 설정
        startPosition = SettingAni.anchoredPosition;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("configNum"+configNum);
            Debug.Log("array"+array);

            configNum++;
            array++;




            StartAnimation();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("configNum" + configNum);
            Debug.Log("array" + array);

            configNum--;
            array--;


            StartAnimation();
        }

        if (isAnimating)
        {
            UpdateAnimation();
        }

        if (configNum > 3)
        {
            configNum = 3;
            array = 3;
        }
        if (configNum < 0)
        {
            configNum = 0;
            array = 0;
        }

    }

    void StartAnimation()
    {
        // 애니메이션 초기화
        startPosition = SettingAni.anchoredPosition;
        elapsedTime = 0f;
        isAnimating = true;
    }

    void UpdateAnimation()
    {
        // 경과 시간 업데이트
        elapsedTime += Time.deltaTime;

        // 현재 경과 시간을 이용해 위치를 Lerp로 계산
        SettingAni.anchoredPosition = Vector2.Lerp(startPosition, new Vector2(0, targetHeight[array]), elapsedTime / duration);

        // 애니메이션이 끝났는지 확인
        if (elapsedTime >= duration)
        {
            // 애니메이션 종료
            SettingAni.anchoredPosition = new Vector2(0, targetHeight[array]);
            isAnimating = false;
        }
    }
}
