using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeInteract : MonoBehaviour
{
    // 상호작용 구역을 참조하기 위한 변수
    public PipeInteractZone pipeInteractZone;
    // 상호작용 여부
    public bool hasInteracted = false; 
    // 느낌표 프리팹
    public GameObject exclamationPrefab; 
    // 흐르는 물 프리팹
    public GameObject floodWaterPrefab;
    // 생성된 느낌표 인스턴스
    private GameObject exclamationInstance; 
    // 생성된 물 인스턴스
    private GameObject floodWaterInstance; 

    void Start()
    {
        if(!hasInteracted)
        {
            //물 생성
            ShowWater();
        }
    }
    void Update()
    {
        // 상호작용 존 안에 있고 상호작용하지 않았다면
        if (pipeInteractZone != null && pipeInteractZone.isPlayerIn && !hasInteracted)
        {
            // 느낌표 생성
            ShowMark(); 
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
        Debug.Log("파이프 상호작용");
        // 물 숨기기
        Destroy(floodWaterInstance);
    }

    // 느낌표 생성 및 표시
    void ShowMark()
    {
        // 이미 표시된 것이 없으면
        if (exclamationInstance == null) 
        {
            // 느낌표 프리팹 생성
            exclamationInstance = Instantiate(exclamationPrefab, transform.position + new Vector3(0, 1.5f, -1.0f), Quaternion.identity);
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

    // 물 이미지 생성
    void ShowWater()
    {
        // 물이 없다면
        if(floodWaterInstance == null)
        {
            // 물 프리팹 이미지 생성
            floodWaterInstance = Instantiate(floodWaterPrefab, new Vector3(-1.25f, -3.19f, -0.57f), Quaternion.identity);
        }
    }
}
