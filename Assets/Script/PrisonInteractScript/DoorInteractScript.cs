using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    // 플레이어를 참조해서 위치를 받아오기 위한 변수
    public Transform playerLocation; 
    // 상호작용 거리
    public float interactionDistance = 1.0f; 
    // 상호작용 여부
    public bool hasInteracted = false; 
    // 느낌표 프리팹
    public GameObject exclamationPrefab; 
    // 문을 참조해서 제거하기 위한 변수
    public GameObject prisonDoor;
    // 생성된 느낌표 인스턴스
    private GameObject exclamationInstance; 

    void Update()
    {
        // 플레이어와 오브젝트 간 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, playerLocation.position);

        // 느낌표 생성
        ShowMark(); 

        // 상호작용 가능한 거리 안에 있고 상호작용하지 않았다면
        if (distanceToPlayer <= interactionDistance && !hasInteracted)
        {
            // 스페이스바로 상호작용
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                Interact(); 
            }
        }
        else 
        {
            // 느낌표 삭제
            HideMark();
        }
    }

    // 상호작용 함수
    void Interact()
    {
        // 나중에 퍼즐 로직을 띄우면 됨
        // puzzle();
        hasInteracted = true;
        HideMark(); 
        // 상호작용 테스트 로그
        Debug.Log("문 상호작용");
        Destroy(prisonDoor);
    }

    // 느낌표 생성 및 표시
    void ShowMark()
    {
        // 이미 표시된 것이 없으면
        if (exclamationInstance == null) 
        {
            // 느낌표 프리팹 생성
            exclamationInstance = Instantiate(exclamationPrefab, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        }
    }

    // 느낌표 숨김
    void HideMark()
    {
        if (exclamationInstance != null)
        {
            // 느낌표 프리팹 삭제
            Destroy(exclamationInstance); 
        }
    }
}
