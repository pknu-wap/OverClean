using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    // UI 요소 연결하기
    public Button QuickMatchButton;
    public Button MakeRoomButton;

    private void Start()
    {
        // 버튼 클릭 이벤트 설정
        QuickMatchButton.onClick.AddListener(OnQuickMatch);
        MakeRoomButton.onClick.AddListener(OnMakeRoom);
    }

    // QuickMatch 버튼 클릭 시 호출
    private void OnQuickMatch()
    {
        // 랜덤 매칭
        if (Photon.Pun.PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("랜덤 방 찾기 시도 중...");
            Photon.Pun.PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.LogWarning("Photon 서버에 연결되지 않았습니다.");
        }
    }

    // 방 생성 버튼 클릭 시 호출
    private void OnMakeRoom()
    {
        // NetworkingManager를 통해 방 생성 요청하기
        NetworkingManager.Instance.CreateRoom();
    }
}
