using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private UIMenuHandler uiLobbyHandler;

    // Lobby Scene에 들어왔을 시
    public override void OnJoinedLobby()
    {
        print("[DEBUG] Callback : OnJoinedLobby()");
        uiLobbyHandler.MenuOpen("Lobby");

        PhotonNetwork.NickName = UserDataManager.GetNickname(true);
    }

    public override void OnCreatedRoom()
    {
        // EMPTY
        print("[DEBUG] Callback : OnCreatedRoom()");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("[DEBUG] Callback : OnCreateRoomFailed()");
    }

    public override void OnJoinedRoom()
    {
        print("[DEBUG] Callback : OnJoinedRoom()");
        if (PhotonNetwork.InLobby) {
            PhotonNetwork.LeaveLobby();
        }
        if (PhotonNetwork.IsMasterClient) {
            print("[DEBUG] Callback : LoadLevel()");
            PhotonNetwork.LoadLevel(3);
        }
    }
    
    public static void CreateRoom(string name, byte maxUser = Constants.DEFAULT_PLAYER_COUNT)
    {
        print("[DEBUG] Execute : CreateRoom()");
        // Call: OnCreateRoom, OnJoinedRoom
        PhotonNetwork.CreateRoom(name, new RoomOptions { MaxPlayers = maxUser });
    }
    
    public static void JoinRoom(string name)
    {
        print("[DEBUG] Execute : JoinRoom()");
        PhotonNetwork.JoinRoom(name);
    }
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}