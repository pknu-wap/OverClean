using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // 상호작용될 오브젝트들 참조(개수 확인용)
    public GameObject[] interactObject; 
    // 최종 도달지점 오브젝트
    public GameObject goalZone;
    // 클리어 조건이 완수되면 표시될 색
    public Color goalZoneColor;
    // 각 오브젝트의 상호작용 상태
    public bool[] interactionsCompleted;
    // 상호작용 완료된 오브젝트 개수
    public int interactCount = 0;

    void Start()
    { 
        // 상호작용 오브젝트 개수만큼 bool 배열 정의
        interactionsCompleted = new bool[interactObject.Length];
    }

    public void ObjectInteract(int index)
    {
        // 만약 상호작용하지 않았는데 함수가 실행된다면
        if(!interactionsCompleted[index])
        {
            // 해당 인덱스 오브젝트 상호작용 완료
            interactionsCompleted[index] = true;
            // 카운트 증가
            interactCount++;
            // 상호작용 수 == 오브젝트 수라면
            if(interactCount == interactObject.Length)
            {
                // 도착지점 활성화
                ActiveGoalZone();
            }
        }
    }

    public void ActiveGoalZone()
    {
        SpriteRenderer sr = goalZone.GetComponent<SpriteRenderer>();
        if(sr != null)
        {
            Debug.Log("색 바뀜");
            // 도착지점의 색을 바꿔서 활성화된 것을 보임
            sr.color = goalZoneColor;
        }
    }
}
