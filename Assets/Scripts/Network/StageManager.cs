using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEvent<Player> playerEnterCallback;
    [SerializeField] private UnityEvent<Player> playerLeftCallback;

    [SerializeField] private UnityEvent stageReadyEvent;
    [SerializeField] private UnityEvent stageCancelEvent;

    [ReadOnly] [SerializeField] private int playerMaxNum;

    [ReadOnly] [SerializeField] private bool isAllowStart;

    private void Awake() {
        playerMaxNum = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerMaxNum = 2;
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
                    photonView.RPC("StageReadyRPC", RpcTarget.AllViaServer);
                }
            }
        }
        else {
            print($"[DEBUG] Callback : OnPlayerEnteredRoom() - Count : {PhotonNetwork.CurrentRoom.PlayerCount}");
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        playerLeftCallback.Invoke(otherPlayer);
        if (PhotonNetwork.CurrentRoom.PlayerCount == playerMaxNum - 1) {
            if (isAllowStart) {
                isAllowStart = false;
                if (PhotonNetwork.IsMasterClient) {
                    photonView.RPC("StageCancelRPC", RpcTarget.AllViaServer);
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

    [PunRPC]
    public void StageReadyRPC() {
        print("[DEBUG] Callback : OnPlayerEnteredRoom() - Room is Full. Game Start~!");
        stageReadyEvent.Invoke();
    }
    
    [PunRPC]
    public void StageCancelRPC() {
        print("[DEBUG] Callback : OnPlayerLeftRoom() - Player is Left. Cancel stage.");
        stageReadyEvent.Invoke();
    }
}