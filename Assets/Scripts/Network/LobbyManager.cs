using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private UIMenuHandler uiLobbyHandler;

    // Lobby Scene에 들어왔을 시
    public override void OnJoinedLobby()
    {
        print("[DEBUG] Method : OnJoinedLobby()");
        uiLobbyHandler.MenuOpen("Lobby");

        PhotonNetwork.NickName = UserDataManager.GetNickname(true);
    }

    public override void OnCreatedRoom()
    {
        // EMPTY
        print("[DEBUG] Method : OnCreatedRoom()");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("[DEBUG] Method : OnCreateRoomFailed()");
    }

    public override void OnJoinedRoom()
    {
        print("[DEBUG] Method : OnJoinedRoom()");
        PhotonNetwork.LeaveLobby();
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel(SceneData.GetSceneName(SceneType.STAGE));
        }
    }
    
    public static void CreateRoom(string name, byte maxUser = Constants.DEFAULT_PLAYER_COUNT)
    {
        print("[DEBUG] Method : CreateRoom()");
        // Call: OnCreateRoom, OnJoinedRoom
        PhotonNetwork.CreateRoom(name, new RoomOptions { MaxPlayers = maxUser });
    }
    
    public static void JoinRoom(string name)
    {
        print("[DEBUG] Class : UIRoom / Method : EnterRoom()");
        PhotonNetwork.JoinRoom(name);
    }
}