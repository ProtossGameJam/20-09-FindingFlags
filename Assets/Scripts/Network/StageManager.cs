using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviourPunCallbacks {
    [SerializeField] private UnityEvent<Player> playerEnterCallback;
    [SerializeField] private UnityEvent<Player> playerLeftCallback;

    [SerializeField] private UnityEvent stageReadyEvent;
    [SerializeField] private UnityEvent stageCancelEvent;

    [ReadOnly, SerializeField] private int playerMaxNum;

    [ReadOnly, SerializeField] private bool isAllowStart;
    [ReadOnly, SerializeField] private bool isGameStart;

    private Coroutine checkRoutine;

    private void Awake() {
        playerMaxNum = PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    private void Start() {
        checkRoutine = StartCoroutine(CheckStartCondition());
    }

    public void StageStart() {
        StopCoroutine(checkRoutine);
        StopAllCoroutines();
        isGameStart = true;
    }

    private IEnumerator CheckStartCondition() {
        if (enabled) {
            yield return new WaitUntil(() => PhotonNetwork.CurrentRoom.PlayerCount >= playerMaxNum);
        }
        Debug.LogWarning(PhotonNetwork.CurrentRoom.PlayerCount + "-" + playerMaxNum);
        if (!isAllowStart) {
            isAllowStart = true;
            if (PhotonNetwork.IsMasterClient) {
                photonView.RPC("StageReadyRPC", RpcTarget.AllViaServer);
            }
        }
    }

    public void LeaveRoom() {
        print("[DEBUG] Method : LeaveRoom()");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() {
        print("[DEBUG] Callback : OnLeftRoom()");
        SceneManager.LoadScene(2);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        playerEnterCallback.Invoke(newPlayer);

        print($"[DEBUG] Callback : OnPlayerEnteredRoom() - Count : {PhotonNetwork.CurrentRoom.PlayerCount}");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        playerLeftCallback.Invoke(otherPlayer);

        if (isAllowStart) {
            isAllowStart = false;
            if (!isGameStart) {
                StopCoroutine(checkRoutine);
                checkRoutine = StartCoroutine(CheckStartCondition());
            }
            if (PhotonNetwork.IsMasterClient) {
                photonView.RPC("StageCancelRPC", RpcTarget.AllViaServer);
            }
        }

        print($"[DEBUG] Callback : OnPlayerLeftRoom() - Count : {PhotonNetwork.CurrentRoom.PlayerCount}");
    }

    public override void OnMasterClientSwitched(Player newMasterClient) {
        print("[DEBUG] Callback : OnMasterClientSwitched()");
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