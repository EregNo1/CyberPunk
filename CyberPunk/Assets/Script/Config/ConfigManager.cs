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

    public float duration = 1f; // �ִϸ��̼� ���� �ð�

    private Vector2 startPosition; // ���� ��ġ
    private float elapsedTime = 0f; // ��� �ð�
    private bool isAnimating = false; // �ִϸ��̼� ����

    public int[] targetHeight;
    int array = 0;



    void Start()
    {
        // �ʱ� ��ġ ����
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
        // �ִϸ��̼� �ʱ�ȭ
        startPosition = SettingAni.anchoredPosition;
        elapsedTime = 0f;
        isAnimating = true;
    }

    void UpdateAnimation()
    {
        // ��� �ð� ������Ʈ
        elapsedTime += Time.deltaTime;

        // ���� ��� �ð��� �̿��� ��ġ�� Lerp�� ���
        SettingAni.anchoredPosition = Vector2.Lerp(startPosition, new Vector2(0, targetHeight[array]), elapsedTime / duration);

        // �ִϸ��̼��� �������� Ȯ��
        if (elapsedTime >= duration)
        {
            // �ִϸ��̼� ����
            SettingAni.anchoredPosition = new Vector2(0, targetHeight[array]);
            isAnimating = false;
        }
    }
}
