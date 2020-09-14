using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    private const string GameVersion = "1";
    
    private void Awake()
    {
        // DEBUG
        Debug.developerConsoleVisible = true;
        
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        if (!PhotonNetwork.IsConnected) {
            print("[DEBUG] Execute : ConnectUsingSettings()");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Photon이 Master 서버에 연결될 시
    public override void OnConnectedToMaster()
    {
        print("[DEBUG] Callback : OnConnectedToMaster()");
        if (!PhotonNetwork.InLobby) {
            print("[DEBUG] Execute : JoinLobby()");
            PhotonNetwork.JoinLobby();
        }
    }
}