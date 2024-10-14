using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeInteractZone : MonoBehaviour
{
    // 플레이어가 들어왔는지 확인하기 위한 변수
    public bool isPlayer1In = false;
    public bool isPlayer2In = false;
    // 현재 상호작용 존 안의 총 플레이어 수 변수
    private int zonePlayerCounter = 0;
    // 시각적으로 플레이어 수를 보여줄 게임오브젝트 변수
    public GameObject pipeInteractZonePlayerCounter1;
    public GameObject pipeInteractZonePlayerCounter2;

    // 플레이어가 구역에 들어온다면 true, 나가면 false (태그로 구분)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            isPlayer1In = true;
            
        }
        else if (other.CompareTag("Player2"))
        {
            isPlayer2In = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            isPlayer1In = false;
            
        }
        else if (other.CompareTag("Player2"))
        {
            isPlayer2In = false;
        }
    }
}
