using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeInteractZone : MonoBehaviour
{
    // 플레이어가 들어왔는지 확인하기 위한 변수
    public bool isPlayer1In = false;
    public bool isPlayer2In = false;

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
