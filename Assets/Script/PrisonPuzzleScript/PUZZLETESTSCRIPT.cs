using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUZZLETESTSCRIPT : MonoBehaviour
{
    private Renderer objectRenderer;

    void Start()
    {
        // 오브젝트의 Renderer 컴포넌트 가져오기
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // T 키를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.T))
        {
            // 오브젝트의 색상을 빨간색으로 변경
            objectRenderer.material.color = Color.red;
            Debug.Log("T키를 눌러서 색상이 빨간색으로 변경되었습니다.");
        }
    }
}
