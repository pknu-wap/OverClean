using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeafInteract : MonoBehaviour
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
    // 낙엽을 참조하기 위한 변수
    public GameObject prisonLeaf;
    // 낙엽을 참조해서 material을 조정하기 위한 spriterenderer 변수
    public SpriteRenderer sr;
    // 나뭇잎 퍼즐
    public GameObject leafPuzzle;
    // 퍼즐이 열려있는지 확인하기 위한 변수
    private bool isPuzzleOpen = false;

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
        // 퍼즐이 열려 있는 상태에서 Z를 누르면 씬을 닫음
        if (isPuzzleOpen && Input.GetKeyDown(KeyCode.Z))
        {
            CloseCurrentPuzzleScene();
        }
    }

    void Interact()
    {
        // 씬매니저로 퍼즐씬 불러오기
        SceneManager.LoadScene("PrisonLeafPuzzleScene", LoadSceneMode.Additive);
        // leafPuzzle 오브젝트 활성화
        if (leafPuzzle != null)
        {
            leafPuzzle.SetActive(true);
            isPuzzleOpen = true;
        }
        stageManager.ObjectInteract(objectIndex);
        HideHighlight();
        Destroy(prisonLeaf);
    }

    void CloseCurrentPuzzleScene()
    {
        leafPuzzle.SetActive(false);
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
}
