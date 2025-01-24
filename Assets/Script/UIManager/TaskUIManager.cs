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
    public List<int> finalTaskCount = new List<int>();
    void Awake()
    {
        for(int i = 0; i < currentTaskCount.Count; i++)
        {
            currentTaskCount[i] = 0;
        }
        // 텍스트 초기화
        for(int i = 0; i < currentTaskCount.Count; i++)
        {
            taskCountList[i].text = string.Format("(" + currentTaskCount[i] + "/" + finalTaskCount[i] + ")");
        }
    }

    public void UpdateCount(int taskIndex)
    {
        // 진행된 태스크 수 증가
        currentTaskCount[taskIndex]++;
        // 텍스트로 갱신
        taskCountList[taskIndex].text = string.Format("(" + currentTaskCount[taskIndex] + "/" + finalTaskCount[taskIndex] + ")");
        // 색깔 갱신(진행 중이라면 노란색)
        if(currentTaskCount[taskIndex] < finalTaskCount[taskIndex])
        {
            taskList[taskIndex].color = Color.yellow;
            taskCountList[taskIndex].color = Color.yellow;
        }
        // 모두 완료됐다면 초록색
        else if(currentTaskCount[taskIndex] == finalTaskCount[taskIndex])
        {
            taskList[taskIndex].color = Color.green;
            taskCountList[taskIndex].color = Color.green;
        }
    }
    
    void Update()
    {
        
    }
}
