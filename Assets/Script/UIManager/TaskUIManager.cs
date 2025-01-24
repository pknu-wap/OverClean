using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUIManager : MonoBehaviour
{

    // 상단 버튼 참조
    public GameObject taskPanelButton;
    // 할 일 목록 패널 참조
    public GameObject taskPanel;
    // 할 일 목록이 열려있는지 체크하는 변수
    private bool taskPanelOpen = false;

    // 할 일 목록 열고 닫는 함수(버튼에 연결)
    void TaskPanelControl()
    {
        if(!taskPanelOpen)
        {
            taskPanelOpen = true;
            taskPanel.gameObject.SetActive(taskPanelOpen);
        }
        else
        {
            taskPanelOpen = false;
            taskPanel.gameObject.SetActive(taskPanelOpen);
        }
    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}
