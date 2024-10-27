using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Test_Chat : MonoBehaviour
{
    public Animator bubble;    // 해당하는 오브젝트 버블의 Animator 컴포넌트
    public dialog_Test dialogTest;
    private bool isChecked = false;     // 스페이스바를 눌렀는지 여부 확인
    private bool isPlayerNear = false;  // 플레이어가 범위 내에 있는지 여부

    void Update()
    {
        // 플레이어가 범위 내에 있을 때 스페이스바 입력 감지
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Space) && !isChecked)
        {
            dialogTest.play_test(); ; // 스페이스바 입력 시 함수 실행
            isChecked = true; // 체크 상태로 변경

            // 애니메이션 변경 (체크 상태)
            bubble.Play("bubble_Checked_ap");
        }
    }

    // 트리거 영역에 플레이어가 들어왔을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isPlayerNear = true; // 플레이어가 범위 내에 있는 상태로 변경

            if (!isChecked)
            {
                // bubble_New_ap 애니메이션 작동
                bubble.Play("bubble_New_ap");
            }
            else
            {
                // 이미 체크된 상태라면 bubble_Checked_ap 작동
                bubble.Play("bubble_Checked_ap");
            }
        }
    }

    // 트리거 영역에서 플레이어가 나갔을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isPlayerNear = false; // 플레이어가 범위 밖으로 나간 상태

            if (!isChecked)
            {
                // bubble_New_dis 애니메이션 작동
                bubble.Play("bubble_New_dis");
            }
            else
            {
                // 이미 체크된 상태라면 bubble_Checked_dis 작동
                bubble.Play("bubble_Checked_dis");
            }
        }
    }

}
