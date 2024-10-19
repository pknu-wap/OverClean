using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NetworkingManager : MonoBehaviourPunCallbacks
{
    // 싱글톤 인스턴스
    public static NetworkingManager Instance;

    // 게임 버전 설정
    private string gameVersion = "1";
    
    // 기존 방 코드를 저장할 HashSet
    private HashSet<string> existingRoomCodes = new HashSet<string>();

    private void Awake()
    {
        // 싱글톤 패턴 구현: NetworkingManager가 중복되지 않도록 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // 씬 전환 시에도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // 게임 시작 시 서버 연결
        ConnectToPhotonServer();
    }

    // Photon 서버에 연결하는 함수
    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            // 씬 자동 동기화
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = gameVersion;
            // Photon 서버 연결
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon Master 서버에 연결되었습니다.");
        PhotonNetwork.JoinLobby();
    }

    // Start 버튼 클릭 시 GameLobby 씬으로 이동
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("GameLobby");
    }

    // Exit 버튼 클릭 시 게임 종료
    public void OnExitButtonClicked()
    {
        // 에디터와 프로그램 실행을 구분.
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // 어플리케이션 종료
            Application.Quit();
        #endif
        Debug.Log("게임 종료");
    }

    // 방 코드 생성 함수
    private string GenerateRoomCode()
    {
        string roomCode;
        // 랜덤 코드 생성하고 중복 확인하기
        do
        {
            roomCode = UnityEngine.Random.Range(1000000, 9999999).ToString();
        } while (existingRoomCodes.Contains(roomCode));

        // 생성된 코드는 여기 저장
        existingRoomCodes.Add(roomCode); 
        return roomCode;
    }

    // 방 생성 요청 함수
    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // 방 코드 생성
            string roomCode = GenerateRoomCode();
            // 방 설정(최대 플레이어 수만 설정함)
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;

            // 방에 저장할 커스텀 프로퍼티(방 코드) 설정
            ExitGames.Client.Photon.Hashtable roomProps = new ExitGames.Client.Photon.Hashtable();
            roomProps.Add("RoomCode", roomCode);
            roomOptions.CustomRoomProperties = roomProps;
            roomOptions.CustomRoomPropertiesForLobby = new string[] { "RoomCode" };

            // 방 이름을 방 코드로 설정하여 방 생성
            PhotonNetwork.CreateRoom(roomCode, roomOptions);
            Debug.Log("생성된 방 코드: " + roomCode);
        }
        else
        {
            Debug.LogWarning("Photon 서버에 아직 연결되지 않았습니다.");
        }
    }

    // 방 입장 요청 함수 (방 코드로 입장)
    public void JoinRoom(string roomCode)
    {
        // 방 코드를 방 이름으로 사용하여 입장 시도
        PhotonNetwork.JoinRoom(roomCode);
    }

    // 방 입장 시 캐릭터 할당
    private void AssignCharacterToPlayer(Photon.Realtime.Player player)
    {
        string assignedCharacter;
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            assignedCharacter = "Dave"; // 첫 번째 플레이어는 Dave
        }
        else
        {
            assignedCharacter = "Matthew"; // 두 번째 플레이어는 Matthew
        }

        ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable();
        playerProps.Add("Character", assignedCharacter);
        player.SetCustomProperties(playerProps);

        Debug.Log(player.NickName + "에게 캐릭터 " + assignedCharacter + "가 할당되었습니다.");
    }

    // 로비에 접속 성공 시 호출
    public override void OnJoinedLobby()
    {
        Debug.Log("로비에 접속했습니다.");
        // 로비에 접속된 플레이어 수 출력
        // 전체 연결된 플레이어 수
        int playerCount = PhotonNetwork.CountOfPlayers;
        Debug.Log("현재 로비에 접속된 플레이어 수: " + playerCount);
    }

    // 방 입장 성공 시 호출
    public override void OnJoinedRoom()
    {
        // 방 코드 가져오기
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("RoomCode", out object roomCode))
        {
            Debug.Log("방 입장 성공: 방 코드 " + roomCode);
        }
        else
        {
            Debug.Log("방 입장 성공: 방 코드를 찾을 수 없습니다.");
        }

        // 플레이어에게 캐릭터 할당
        AssignCharacterToPlayer(PhotonNetwork.LocalPlayer);

        // 방에 입장 성공하면 씬 전환
        SceneManager.LoadScene("Room");
    }

    // 방 입장 실패 시 호출 (방 코드로 입장 실패 시)
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("방 입장 실패: " + message);
    }

    // 방 생성 실패 시 호출
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("방 생성 실패: " + message);
    }

    // 플레이어가 방에서 나갈 때 호출
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + "이(가) 방을 나갔습니다.");
        // 추가 로직이 필요한 경우 여기에 구현
    }

    // 방에서 나가는 경우에 호출
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    // 플레이어 전환을 처리하는 함수
    public void SwitchPlayers()
    {
        // 로컬 플레이어의 캐릭터 속성 가져오기
        var localPlayer = PhotonNetwork.LocalPlayer;
        string currentCharacter = localPlayer.CustomProperties.ContainsKey("Character")
                                ? localPlayer.CustomProperties["Character"].ToString() : "";

        // 상대 플레이어 찾기
        Photon.Realtime.Player otherPlayer = null;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (!player.IsLocal) // 로컬 플레이어가 아닌 상대 플레이어 찾기
            {
                otherPlayer = player;
                break;
            }
        }

        // 전환할 상대 플레이어가 없으면 리턴
        if (otherPlayer == null) return;

        // 상대 플레이어의 캐릭터 속성 가져오기
        string otherCharacter = otherPlayer.CustomProperties.ContainsKey("Character")
                                ? otherPlayer.CustomProperties["Character"].ToString() : "";

        // 로컬 플레이어와 상대 플레이어의 캐릭터 교환
        if (!string.IsNullOrEmpty(currentCharacter) && !string.IsNullOrEmpty(otherCharacter))
        {
            // 로컬 플레이어는 상대의 캐릭터를, 상대는 로컬의 캐릭터를 할당
            // System.Collections.Hashtable과 충돌 여지가 있어서 네임스페이스를 명시적으로 사용했습니다.
            ExitGames.Client.Photon.Hashtable localPlayerProperties = new ExitGames.Client.Photon.Hashtable { { "Character", otherCharacter } };
            ExitGames.Client.Photon.Hashtable otherPlayerProperties = new ExitGames.Client.Photon.Hashtable { { "Character", currentCharacter } };

            localPlayer.SetCustomProperties(localPlayerProperties);
            otherPlayer.SetCustomProperties(otherPlayerProperties);

            Debug.Log($"플레이어 전환 완료: localPlayer는 {otherCharacter}, OnlinePlayer는 {currentCharacter}");

            // 추가: 캐릭터 전환 후 RoomManager의 캐릭터 이미지 업데이트 호출
            RoomManager roomManager = FindObjectOfType<RoomManager>();
            if (roomManager != null)
            {
                roomManager.UpdateCharacterImages();
            }
        }
    }
}