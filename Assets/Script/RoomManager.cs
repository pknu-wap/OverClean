using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // 방 코드 표시 텍스트
    public Text roomCodeText;
    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            // 방에 입장 시 방 코드 표시
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("RoomCode"))
            {
                roomCodeText.text = "입장 코드 : " + PhotonNetwork.CurrentRoom.CustomProperties["RoomCode"].ToString();
            }
        }
    }

    // Ready 버튼 클릭 시 호출되는 함수
    public void ReadyUp()
    {
        // 플레이어가 준비 상태임을 서버에 알리는 로직 추가 가능
        Debug.Log("Player Ready!");
    }

    // Back 버튼 클릭 시 호출되는 함수
    public void LeaveRoom()
    {
        // 방을 떠나는 작업을 먼저 수행
        PhotonNetwork.LeaveRoom();
        Debug.Log("방을 나가는 중입니다...");
    }

    // 방을 나가면 호출되는 콜백
    public override void OnLeftRoom()
    {
        // 방을 나간 후에 GameLobby 씬으로 전환
        Debug.Log("방을 떠났습니다.");
        SceneManager.LoadScene("GameLobby");
    }
}
