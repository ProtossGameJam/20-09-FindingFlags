using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEvent onConnectedEvent;
    [SerializeField] private UnityEvent onJoinLobbyEvent;
    
    private void Awake() {
        PhotonNetwork.GameVersion = Constants.GAME_NETWORK_VERSION;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void ConnectToServer() {
        if (PhotonNetwork.IsConnected) return;
        print("[DEBUG] Execute : ConnectUsingSettings()");
        PhotonNetwork.ConnectUsingSettings();
    }

    // Photon이 Master 서버에 연결될 시
    public override void OnConnectedToMaster() {
        print("[DEBUG] Callback : OnConnectedToMaster()");
        onConnectedEvent.Invoke();
        if (!PhotonNetwork.InLobby) {
            print("[DEBUG] Execute : JoinLobby()");
            PhotonNetwork.JoinLobby();
        }
    }

    // Lobby Scene에 들어왔을 시
    public override void OnJoinedLobby() {
        print("[DEBUG] Callback : OnJoinedLobby()");
        onJoinLobbyEvent.Invoke();
    }
}