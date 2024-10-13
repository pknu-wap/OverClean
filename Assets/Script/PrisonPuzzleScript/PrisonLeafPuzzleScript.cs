using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrisonLeafPuzzleScript : MonoBehaviour
{
    void Update()
    {
        // Z 키를 눌렀을 때 씬 닫기
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ClosePuzzleScene();
        }
    }

    // 씬 닫기 함수
    void ClosePuzzleScene()
    {
        // 현재 씬 닫기
        SceneManager.UnloadSceneAsync("PrisonLeafPuzzleScene");
        Debug.Log("씬이 닫혔습니다.");
    }
}
