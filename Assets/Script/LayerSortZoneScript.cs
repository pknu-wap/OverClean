using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerAdjust : MonoBehaviour
{
    // 두 플레이어의 Transform을 참조할 변수
    public Transform matthewTransform;
    public Transform daveTransform;

    // 플레이어가 구역에 들어올 때
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player1이 구역에 들어왔는지 확인
        if (other.CompareTag("Player1"))
        {
            matthewTransform.position = new Vector3(matthewTransform.position.x, matthewTransform.position.y, matthewTransform.position.z - 0.11f);
            Debug.Log("Player1이 구역에 들어옴: 레이어 -0.11f");
        }
        // Player2가 구역에 들어왔는지 확인
        else if (other.CompareTag("Player2"))
        {
            daveTransform.position = new Vector3(daveTransform.position.x, daveTransform.position.y, daveTransform.position.z - 0.11f);
            Debug.Log("Player2가 구역에 들어옴: 레이어 -0.11f");
        }
    }

    // 플레이어가 구역에서 나갈 때
    private void OnTriggerExit2D(Collider2D other)
    {
        // Player1이 구역에서 나갔는지 확인
        if (other.CompareTag("Player1"))
        {
            matthewTransform.position = new Vector3(matthewTransform.position.x, matthewTransform.position.y, matthewTransform.position.z + 0.11f);
            Debug.Log("Player1이 구역에서 나감: 레이어 +0.11f");
        }
        // Player2가 구역에서 나갔는지 확인
        else if (other.CompareTag("Player2"))
        {
            daveTransform.position = new Vector3(daveTransform.position.x, daveTransform.position.y, daveTransform.position.z + 0.11f);
            Debug.Log("Player2가 구역에서 나감: 레이어 +0.11f");
        }
    }
}
