using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEvent<Player> playerEnterCallback;
    [SerializeField] private UnityEvent<Player> playerLeftCallback;
    
    [SerializeField] private UnityEvent         stageStartEvent;
    [SerializeField] private UnityEvent         stageCancelEvent;

    [ReadOnly] [SerializeField] private int playerMaxNum;
    
    [ReadOnly] [SerializeField] private bool isAllowStart = false;

    private void Awake() {
        playerMaxNum = PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnLeftRoom() {
        print("[DEBUG] Callback : OnLeftRoom()");
        SceneManager.LoadScene(2);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        playerEnterCallback.Invoke(newPlayer);
        if (PhotonNetwork.CurrentRoom.PlayerCount >= playerMaxNum) {
            if (!isAllowStart) {
                isAllowStart = true;
                if (PhotonNetwork.IsMasterClient) {
                    print($"[DEBUG] Callback : OnPlayerEnteredRoom() - Room is Full. Game Start~!");
                    stageStartEvent.Invoke();
                }
            }
        }
        else {
            print($"[DEBUG] Callback : OnPlayerEnteredRoom() - Count : {PhotonNetwork.CurrentRoom.PlayerCount}");
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        playerLeftCallback.Invoke(otherPlayer);
        if (playerMaxNum - PhotonNetwork.CurrentRoom.PlayerCount <= 1) {
            if (isAllowStart) {
                isAllowStart = false;
                if (PhotonNetwork.IsMasterClient) {
                    print($"[DEBUG] Callback : OnPlayerLeftRoom() - Player is Left. Cancel stage.");
                    stageCancelEvent.Invoke();
                }
            }
        }
        else {
            print($"[DEBUG] Callback : OnPlayerLeftRoom() - Count : {PhotonNetwork.CurrentRoom.PlayerCount}");
        }
    }

    // Called when Master Client left room
    public override void OnMasterClientSwitched(Player newMasterClient) {
        // EMPTY
        print("[DEBUG] Callback : OnMasterClientSwitched()");
    }

    public void LeaveRoom() {
        // Call: OnLeftRoom
        print("[DEBUG] Method : LeaveRoom()");
        PhotonNetwork.LeaveRoom();
    }
}