using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEvent<Player> playerEnterCallback;
    [SerializeField] private UnityEvent<Player> playerLeftCallback;
    
    public override void OnLeftRoom()
    {
        print("[DEBUG] Method : OnLeftRoom()");
        PhotonNetwork.JoinLobby();
        SceneLoader.LoadScene(SceneType.LOBBY);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("[DEBUG] OnPlayerEnteredRoom()");
        playerEnterCallback.Invoke(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("[DEBUG] OnPlayerLeftRoom()");
        playerLeftCallback.Invoke(otherPlayer);
    }

    // Called when Master Client left room
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        // EMPTY
        print("[DEBUG] OnMasterClientSwitched()");
    }
    
    public void LeaveRoom()
    {
        // Call: OnLeftRoom
        print("[DEBUG] LeaveRoom()");
        PhotonNetwork.LeaveRoom();
    }
}