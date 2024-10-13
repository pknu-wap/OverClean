using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeInteract : MonoBehaviour
{
    // 테두리 없는 상태
    public Material normalState;
    // 테두리 있는 상태
    public Material canInteractState;
    // 오브젝트의 인덱스(감옥 맵에서 0~7)
    public int objectIndex;
    // stagemanager를 참조해서 상호작용 여부를 제어하기 위한 변수
    public StageManager stageManager;
    // 상호작용 구역을 참조하기 위한 변수
    public PipeInteractZone pipeInteractZone;
    // 상호작용 여부
    public bool hasInteracted = false; 
    // 흐르는 물 프리팹
    public GameObject floodWaterPrefab;
    // 생성된 흐르는 물 인스턴스
    private GameObject floodWaterInstance; 
    // 파이프를 참조해서 material을 조정하기 위한 spriterenderer 변수
    public SpriteRenderer sr;
    // 파이프 퍼즐
    public GameObject pipePuzzle;
    // 퍼즐이 열려있는지 확인하기 위한 변수
    private bool isPuzzleOpen = false;

    void Start()
    {
        if(!hasInteracted)
        {
            //물 생성
            ShowWater();
        }
        // start 혹은 awake에서 sr을 getcomponent 메서드로 초기화
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // 상호작용 존 안에 두 플레이어 모두가 있고 상호작용하지 않았다면
        if (pipeInteractZone != null && pipeInteractZone.isPlayer1In && pipeInteractZone.isPlayer2In && !hasInteracted)
        {
            // 테두리 생성
            ShowHighlight(); 
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
        SceneManager.LoadScene("PrisonPipePuzzleScene", LoadSceneMode.Additive);
        // pipePuzzle 오브젝트 활성화
        if (pipePuzzle != null)
        {
            pipePuzzle.SetActive(true);
            isPuzzleOpen = true;
        }
        stageManager.ObjectInteract(objectIndex);
        HideHighlight();
        // 물 숨기기
        Destroy(floodWaterInstance);
    }

    void CloseCurrentPuzzleScene()
    {
        pipePuzzle.SetActive(false);
        // 퍼즐 닫힘을 표시
        isPuzzleOpen = false;
        // 상호작용되었음을 표시
        hasInteracted = true;
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
