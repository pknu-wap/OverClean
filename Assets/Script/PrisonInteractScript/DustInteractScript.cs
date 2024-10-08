using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustInteract : MonoBehaviour
{
    // 테두리 없는 상태
    public Material normalState;
    // 테두리 있는 상태
    public Material canInteractState;
    // 오브젝트의 인덱스(감옥 맵에서 0~7)
    public int objectIndex;
    // stagemanager를 참조해서 상호작용 여부를 제어하기 위한 변수
    public StageManager stageManager;
    // 플레이어를 참조해서 위치를 받아오기 위한 변수
    public Transform playerLocation; 
    // 상호작용 거리
    public float interactionDistance = 1.0f; 
    // 상호작용 여부
    public bool hasInteracted = false; 
    // 먼지를 참조하기 위한 변수
    public GameObject prisonDust;
    // 먼지를 참조해서 material을 조정하기 위한 spriterenderer 변수
    public SpriteRenderer sr;

    void Start()
    {
        // sr을 getcomponent 메서드로 초기화
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // 플레이어와 오브젝트 간 거리 계산
        float distanceToPlayer = Vector3.Distance(transform.position, playerLocation.position);

        // 테두리 생성
        ShowHighlight(); 

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
            // 테두리 삭제
            HideHighlight();
        }
    }

    // 상호작용 함수
    void Interact()
    {
        // 나중에 퍼즐 로직을 띄우면 됨
        // puzzle();
        // statemanager에게 상호작용되었다고 알림
        stageManager.ObjectInteract(objectIndex);
        hasInteracted = true;
        HideHighlight(); 
        // 상호작용 테스트 로그
        Debug.Log("먼지 상호작용");
        Destroy(prisonDust);
    }

    // 테두리 생성 및 표시
    void ShowHighlight()
    {
        // 테두리가 있는 material로 변경
        sr.material = canInteractState;
    }

    // 테두리 숨김
    void HideHighlight()
    {
        // 테두리가 없는 material로 변경
        sr.material = normalState;
    }
}
