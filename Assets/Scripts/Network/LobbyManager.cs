using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private UIMenuHandler uiLobbyHandler;

    private void Start() {
        uiLobbyHandler.MenuOpen("Lobby");
        PhotonNetwork.NickName = UserDataManager.GetNickname(true);
    }

    // Photon이 Master 서버에 연결될 시
    public override void OnConnectedToMaster() {
        print("[DEBUG] Callback : OnConnectedToMaster()");
        if (!PhotonNetwork.InLobby) PhotonNetwork.JoinLobby();
    }

    public override void OnCreatedRoom() { print("[DEBUG] Callback : OnCreatedRoom()"); }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        print("[DEBUG] Callback : OnCreateRoomFailed()");
    }

    public override void OnJoinedRoom() {
        print("[DEBUG] Callback : OnJoinedRoom()");
        if (PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();
        if (PhotonNetwork.IsMasterClient) {
            print("[DEBUG] Callback : LoadLevel()");
            PhotonNetwork.LoadLevel(3);
        }
    }

    public static void CreateRoom(string name, byte maxUser = Constants.DEFAULT_PLAYER_COUNT) {
        print("[DEBUG] Execute : CreateRoom()");
        // Call: OnCreateRoom, OnJoinedRoom
        PhotonNetwork.CreateRoom(name, new RoomOptions {MaxPlayers = maxUser});
    }

    public static void JoinRoom(string name) {
        print("[DEBUG] Execute : JoinRoom()");
        PhotonNetwork.JoinRoom(name);
    }

    public void ExitGame() {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}