using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
    // 플레이어 1이 구역에 들어왔는지 여부
    private bool player1InZone = false; 
    // 플레이어 2가 구역에 들어왔는지 여부 
    // private bool player2InZone = false; 
    // 상호작용 개수를 확인하기 위한 stagemanager 참조
    public StageManager stageManager;    

    // 플레이어가 구역에 들어왔을 때 처리
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InZone = true;
            Debug.Log("플레이어 1 구역 도착");
            CheckForClear();
        }
        /*
        else if (other.CompareTag("Player2"))
        {
            player2InZone = true;
            Debug.Log("플레이어 2 구역 도착");
            CheckForClear();
        }
        */
    }

    // 플레이어가 구역에서 나갔을 때 처리
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1InZone = false;
        }
        /*
        else if (other.CompareTag("Player2"))
        {
            player2InZone = false;
        }
        */
    }

    void CheckForClear()
    {
        // 두 플레이어가 구역 안에 들어왔고, 모든 상호작용이 완료되었을 때
        if (player1InZone /*&& player2InZone*/ && stageManager.interactCount == stageManager.interactObject.Length)
        {
            ClearStage();
        }
    }

    void ClearStage()
    {
        // 스테이지 클리어
        Debug.Log("스테이지 클리어");
        // 여기에 게임 클리어 처리 로직 추가
    }
}
