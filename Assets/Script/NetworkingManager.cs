using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkingManager : MonoBehaviourPunCallbacks
{
    // 싱글톤 인스턴스
    public static NetworkingManager Instance;

    // 게임 버전 설정
    private string gameVersion = "1";

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

    // Photon 서버에 연결 성공 시 호출
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon 서버에 연결되었습니다.");
    }
