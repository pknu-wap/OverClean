using UnityEngine;
using UnityEngine.UI; // UI 요소를 다루기 위해 필요
using Photon.Pun;

public class UIManager : MonoBehaviour
{
    public Button switchButton; // Inspector에서 버튼을 연결

    private void Start()
    {
        // NetworkingManager가 로드될 때까지 기다린 후, 버튼 이벤트 연결
        if (PhotonNetwork.IsConnected)
        {
            if (NetworkingManager.Instance != null)
            {
                switchButton.onClick.AddListener(NetworkingManager.Instance.SwitchPlayers);
            }
        }
        else
        {
            // Photon 서버에 연결되어 있지 않을 때의 처리 로직
            Debug.LogWarning("Photon 서버에 연결되어 있지 않음.");
        }
    }
}
