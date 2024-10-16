using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrisonDoorLockScript : MonoBehaviour
{
    // 퍼즐 매니저 참조
    public PrisonDoorPuzzleScript doorPuzzleManager;
    // 정답 키 변수
    public GameObject ansKey;

    // 퍼즐 매니저를 프리팹에 참조시킴
    void Awake()
    {
        doorPuzzleManager = FindObjectOfType<PrisonDoorPuzzleScript>();
    }

    // 열쇠와 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 만약 해답 열쇠라면
        if(other.GetComponent<PrisonDoorKeyScript>().isAnsKey)
        {
            Debug.Log("맞는 열쇠, 퍼즐 해결 성공");
            // 퍼즐 매니저에게 해결 신호 전달
            doorPuzzleManager.puzzleSolved = true;
        }
        else
        {
            // 여기에 일정시간 못 시도하게 스턴..? 로직 넣어도 될듯?
            Debug.Log("틀린 열쇠");
        }
    }
}
