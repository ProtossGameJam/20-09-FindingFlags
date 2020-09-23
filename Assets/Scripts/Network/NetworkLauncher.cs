using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class NetworkLauncher : MonoBehaviourPunCallbacks {
    [SerializeField] private int sendRate = 60;
    [SerializeField] private int serializationRate = 30;

    [SerializeField] private UnityEvent onConnectedEvent;
    [SerializeField] private UnityEvent onJoinLobbyEvent;

    private void Awake() {
        PhotonNetwork.GameVersion = Constants.GAME_NETWORK_VERSION;
        PhotonNetwork.SendRate = sendRate;
        PhotonNetwork.SerializationRate = serializationRate;
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