using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Test_Chat : MonoBehaviour
{
    public Animator bubble;    // �ش��ϴ� ������Ʈ ������ Animator ������Ʈ
    public dialog_Test dialogTest;
    private bool isChecked = false;     // �����̽��ٸ� �������� ���� Ȯ��
    private bool isPlayerNear = false;  // �÷��̾ ���� ���� �ִ��� ����

    void Update()
    {
        // �÷��̾ ���� ���� ���� �� �����̽��� �Է� ����
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space) && !isChecked)
        {
            dialogTest.play_test(); ; // �����̽��� �Է� �� �Լ� ����
            isChecked = true; // üũ ���·� ����

            // �ִϸ��̼� ���� (üũ ����)
            bubble.Play("bubble_Checked_ap");
        }
    }

    // Ʈ���� ������ �÷��̾ ������ ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isPlayerNear = true; // �÷��̾ ���� ���� �ִ� ���·� ����

            if (!isChecked)
            {
                // bubble_New_ap �ִϸ��̼� �۵�
                bubble.Play("bubble_New_ap");
            }
            else
            {
                // �̹� üũ�� ���¶�� bubble_Checked_ap �۵�
                bubble.Play("bubble_Checked_ap");
            }
        }
    }

    // Ʈ���� �������� �÷��̾ ������ ��
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isPlayerNear = false; // �÷��̾ ���� ������ ���� ����

            if (!isChecked)
            {
                // bubble_New_dis �ִϸ��̼� �۵�
                bubble.Play("bubble_New_dis");
            }
            else
            {
                // �̹� üũ�� ���¶�� bubble_Checked_dis �۵�
                bubble.Play("bubble_Checked_dis");
            }
        }
    }

}
