using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract : MonoBehaviour
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
    // 문을 참조하기 위한 변수
    public GameObject prisonDoor;
    // 문을 이동시킬 목표 위치 targetPosition 선언
    private Vector3 targetPosition;
    // 문 이동 속도
    public float doorMoveSpeed = 0.1f; 
    // 문이 이동 중인지 여부
    private bool isMoving = false; 
    // 문을 참조해서 material을 조정하기 위한 spriterenderer 변수
    public SpriteRenderer sr;
    // 문 퍼즐
    public GameObject doorPuzzle;
    // 퍼즐이 열려있는지 확인하기 위한 변수
    private bool isPuzzleOpen = false;

    void Awake()
    {
        // sr을 getcomponent 메서드로 초기화
        sr = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        // targetPosition 초기화
        targetPosition = new Vector3(prisonDoor.transform.position.x - 1.4f, prisonDoor.transform.position.y, prisonDoor.transform.position.z);
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
        // isMoving이면 문을 이동시키는 애니메이션 함수 작동
        if (isMoving)
        {
            MoveDoor();
        }
        // 퍼즐이 열려 있는 상태에서 Z를 누르면 씬을 닫음
        if (isPuzzleOpen && Input.GetKeyDown(KeyCode.Z))
        {
            CloseCurrentPuzzleScene();
        }
    }

    // 상호작용 함수
    void Interact()
    {
        // 씬매니저로 퍼즐씬 불러오기
        SceneManager.LoadScene("PrisonDoorPuzzleScene", LoadSceneMode.Additive);
        // doorPuzzle 오브젝트 활성화
        if (doorPuzzle != null)
        {
            doorPuzzle.SetActive(true);
            isPuzzleOpen = true;
        }
        // statemanager에게 상호작용되었다고 알림
        stageManager.ObjectInteract(objectIndex);
        // 문 이동 시작
        isMoving = true; 
        HideHighlight(); 
    }

    void CloseCurrentPuzzleScene()
    {
        doorPuzzle.SetActive(false);
        // 퍼즐 닫힘을 표시
        isPuzzleOpen = false;
        // 상호작용되었음을 표시
        hasInteracted = true;
    }

    // 문을 부드럽게 이동시키는 함수
    void MoveDoor()
    {
        // 문을 타겟 위치까지 부드럽게 이동시킴
        prisonDoor.transform.position = Vector3.MoveTowards(prisonDoor.transform.position, targetPosition, doorMoveSpeed * Time.deltaTime);

        // 문이 목표 위치에 도달하면 이동 중지
        if (Vector3.Distance(prisonDoor.transform.position, targetPosition) < 0.01f)
        {
            isMoving = false;
            Debug.Log("문 열림 완료");
        }
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
