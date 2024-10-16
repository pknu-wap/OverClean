using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrisonDoorLockScript : MonoBehaviour
{
    // 퍼즐 매니저 참조
    public PrisonDoorPuzzleScript doorPuzzleManager;
    // 정답 키 변수
    private GameObject ansKey;

    void Start()
    {
        // 퍼즐 매니저가 지정한 정답 키 할당
        ansKey = doorPuzzleManager.ansKey;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other == ansKey)
        {
            Debug.Log("맞는 열쇠, 퍼즐 해결 성공");
            doorPuzzleManager.puzzleSolved = true;
        }
        else
        {
            // 여기에 일정시간 못 시도하게 스턴..? 로직 넣어도 될듯?
            Debug.Log("틀린 열쇠");
        }
    }
}
