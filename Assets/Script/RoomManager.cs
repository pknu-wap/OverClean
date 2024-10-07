using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

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
        PhotonNetwork.LeaveRoom();
        Debug.Log("방을 나갔습니다.");
    }
}
