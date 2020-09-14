using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkLauncher : MonoBehaviourPunCallbacks
{
    private const string GameVersion = "1";
    
    private void Awake()
    {
        PhotonNetwork.GameVersion = GameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        if (!PhotonNetwork.IsConnected) {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Photon이 Master 서버에 연결될 시
    public override void OnConnectedToMaster()
    {
        print("[DEBUG] Method : OnConnectedToMaster()");
        PhotonNetwork.JoinLobby();
    }
}