using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeInteractZone : MonoBehaviour
{
    // 플레이어가 들어왔는지 확인하기 위한 변수
    public bool isPlayerIn = false;
    // 플레이어가 구역에 들어온다면 true, 나가면 false
    private void OnTriggerEnter2D(Collider2D other)
    {
        isPlayerIn = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerIn = false;
    }
}
