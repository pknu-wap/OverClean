using System.Collections;
using System.Collections.Generic;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrisonDoorPuzzleScript : MonoBehaviour
{
    // 자물쇠 목록
    public List<GameObject> lockObjectsList = new List<GameObject>();
    // 열쇠 목록
    public List<GameObject> keyObjectsList = new List<GameObject>();

    // 선택한 자물쇠와 맞는 열쇠 변수
    public GameObject ansLock;
    public GameObject ansKey;

    // 표시될 열쇠들의 목록(일단 7개, 코드에서 수정가능)
    private GameObject[] displayKeyObjectsList = new GameObject[7];

    // 열쇠가 생성될 구간
    private Vector2 minPosition = new Vector2(-58.0f, -56.0f);
    private Vector2 maxPosition = new Vector2(-42.0f, -50.0f);

    // 퍼즐이 풀렸는지 자물쇠로부터 정보를 받아올 변수(초기값 false)
    public bool puzzleSolved = false;

    void Start()
    {
        // 해답이 될 자물쇠 - 열쇠 쌍 인덱스 랜덤으로 선택
        int choosenIndex = Random.Range(0,lockObjectsList.Count - 1);
        // 사용될 자물쇠 할당
        ansLock = lockObjectsList[choosenIndex];
        // 자물쇠에 맞는 열쇠 할당
        ansKey = keyObjectsList[choosenIndex];
        // 열쇠 스크립트의 해답 속성 true로 변경
        ansKey.GetComponent<PrisonDoorKeyScript>().isAnsKey = true;
        // 오답 열쇠 6개 생성 반복문
        for(int i = 0; i < 6; i++)
        {
            // 생성될 열쇠의 인덱스
            int keyIndex;
            do
            {
                // 0~4 인덱스 사이에서 뽑되, 정답 열쇠는 하나만 존재해야 하므로 해답 인덱스는 제외
                keyIndex = Random.Range(0,5);
            } while(keyIndex == choosenIndex);
            GenerateKey(keyIndex);
        }
        // 해답 열쇠 생성
        GenerateKey(choosenIndex);
        // 자물쇠 생성
        Instantiate(lockObjectsList[choosenIndex], new Vector3(-50.0f,-47.0f,-1.1f), Quaternion.identity);
    }

    void Update()
    {
        // Z 키를 눌렀을 때 퍼즐 성공
        if (puzzleSolved)
        {
            PuzzleSuccess();
        }

        // X 키를 눌렀을 때 씬 닫기
        // 추후 퍼즐 닫기 같은 버튼 UI와 연결..?
        if (Input.GetKeyDown(KeyCode.X))
        {
            ClosePuzzleScene();
        }
    }

    // 키 인덱스를 매개변수로 받아 키 인스턴스 생성
    public void GenerateKey(int curKeyIndex)
    {
        Vector3 keyGeneratePosition = new Vector3(
                // x축 랜덤 위치
                Random.Range(minPosition.x, maxPosition.x), 
                // y축 랜덤 위치
                Random.Range(minPosition.y, maxPosition.y), 
                // z축은 -1.1f로 고정
                -1.1f  
            );
            // 키 생성
            Instantiate(keyObjectsList[curKeyIndex], keyGeneratePosition, Quaternion.identity);
    }

    // 퍼즐 성공 시 호출되는 함수
    public void PuzzleSuccess()
    {
        // 퍼즐 매니저의 puzzlesuccess 호출
        PuzzleManager.instance.PuzzleSuccess();
        // 더이상 씬을 열 필요가 없으니 씬 닫기. 중간에 UI 삽입을 위한 시간을 추가해도 될듯?
        ClosePuzzleScene();
    }

    // 씬 닫기 함수
    void ClosePuzzleScene()
    {
        // 현재 씬 닫기
        SceneManager.UnloadSceneAsync("PrisonDoorPuzzleScene");
        Debug.Log("씬이 닫혔습니다.");
    }
}
