using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : PunSingleton<LobbyManager>
{
    [SerializeField] private UIMenuHandler uiLobbyHandler;
    [SerializeField] private UIRoomList uiRoomList;
    
    private const string GameVersion = "1";
    
    private void Awake()
    {
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Photon이 Master 서버에 연결될 시
    public override void OnConnectedToMaster()
    {
        print("[DEBUG] Method : OnConnectedToMaster()");
        PhotonNetwork.JoinLobby();
    }
    
    // Lobby Scene에 들어왔을 시
    public override void OnJoinedLobby()
    {
        print("[DEBUG] Method : OnJoinedLobby()");
        uiLobbyHandler.MenuOpen("Lobby");

        PhotonNetwork.NickName = UserDataManager.GetNickname(true);
    }

    public void CreateRoom(string name, byte maxUser = Constants.DEFAULT_PLAYER_COUNT)
    {
        print("[DEBUG] Method : CreateRoom()");
        // Call: OnCreateRoom, OnJoinedRoom
        PhotonNetwork.CreateRoom(name, new RoomOptions { MaxPlayers = maxUser });
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("[DEBUG] Method : OnCreateRoomFailed()");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public override void OnJoinedRoom()
    {
        print("[DEBUG] Method : OnJoinedRoom()");
        PhotonNetwork.LoadLevel(SceneHandler.GetSceneName(SceneType.STAGE));
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) //방 리스트가 업데이트 될 때
    {
        uiRoomList.SettingRoom(roomList);
    }
}