using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TaskUIManager : MonoBehaviour
{
    // 태스크 목록을 관리할 텍스트 UI 배열 (내용 텍스트)
    public List<TMP_Text> taskList = new List<TMP_Text>();
    // 태스크 카운트 목록을 관리할 텍스트 UI 배열 (숫자 텍스트)
    public List<TMP_Text> taskCountList = new List<TMP_Text>();
    // 현재 태스크 진행도 저장 배열
    public List<int> currentTaskCount = new List<int>();
    // 최종 태스크 개수 저장 배열
    public List<int> finalTaskCount;
    void Awake()
    {
        for(int i = 0; i < currentTaskCount.Count; i++)
        {
            currentTaskCount[i] = 0;
        }
        // 감옥 맵일 경우, 문 / 파이프 / 먼지 / 낙엽 퍼즐 개수는 2 / 1 / 4 / 1
        if(SceneManager.GetActiveScene().name == "PrisonScene")
        {
            finalTaskCount = new List<int> {2,1,4,1};
        }
        // 주택 씬일 경우, 모든 퍼즐 개수는 1로 동일 (8개 리스트 생성)
        else if(SceneManager.GetActiveScene().name == "HouseScene")
        {
            finalTaskCount = new List<int> {1,1,1,1,1,1,1,1};
        }
        // 텍스트 초기화
        for(int i = 0; i < currentTaskCount.Count; i++)
        {
            taskCountList[i].text = string.Format("(" + currentTaskCount[i] + "/" + finalTaskCount[i] + ")");
        }
    }

    
    
    void Update()
    {
        
    }
}
