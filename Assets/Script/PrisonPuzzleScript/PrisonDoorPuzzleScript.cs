using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrisonDoorPuzzleScript : MonoBehaviour
{
    // 자물쇠 목록
    public List<GameObject> lockObjects = new List<GameObject> {};
    // 열쇠 목록
    public List<GameObject> keyObjects = new List<GameObject> {};

    // 선택한 자물쇠와 맞는 열쇠 변수
    private GameObject curLock;
    private GameObject curkey;
    
    void Update()
    {
        // Z 키를 눌렀을 때 퍼즐 성공
        if (Input.GetKeyDown(KeyCode.Z))
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
