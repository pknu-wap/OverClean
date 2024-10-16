using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonDoorKeyScript : MonoBehaviour
{
    // 이 키가 해답 키인지 확인하는 변수(초기값 false)
    public bool isAnsKey = false;
    // 마우스 위치 - 오브젝트 거리 오프셋 저장 변수
    private Vector3 offset;
    // 메인카메라 참조 변수
    private Camera mainCamera;
    // 오브젝트가 드래그 중인지 여부 확인 변수
    private bool isDragging = false;

    void Start()
    {
        // 주 카메라 참조
        mainCamera = Camera.main;
    }

    // 마우스로 클릭했을 때 드래그 시작
    void OnMouseDown()
    {
        isDragging = true;

        // 마우스 위치를 월드 좌표로 변환한 후, 오브젝트와의 상대적 위치 계산
        Vector3 mousePosition = GetMouseWorldPosition();
        offset = transform.position - mousePosition;
    }

    // 마우스 클릭해제 시 드래그 중지
    void OnMouseUp()
    {
       
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // 마우스가 움직이는 대로 오브젝트를 이동
            Vector3 mousePosition = GetMouseWorldPosition();
            transform.position = mousePosition + offset;
        }
    }

    // 화면의 마우스 위치를 월드 좌표로 변환하는 함수
    Vector3 GetMouseWorldPosition()
    {
        // 현재 마우스의 화면 좌표를 가져옴
        Vector3 mousePosition = Input.mousePosition;
        // 카메라의 Z축 거리를 고려, 마우스 좌표의 Z축 값 설정
        mousePosition.z = Mathf.Abs(mainCamera.transform.position.z);
        // 화면 좌표를 월드 좌표로 변환
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
