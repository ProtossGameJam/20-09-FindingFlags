using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public void StartGame()
    {
        
    }
    
    public void LeaveRoom()
    {
        // Call: OnLeftRoom
        PhotonNetwork.LeaveRoom();
        UIDebugText.Logging("LeaveRoom()");
    }
    
    public override void OnLeftRoom()
    {
        SceneHandler.LoadScene(SceneType.LOBBY);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }

    // Called when Master Client left room
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        
    }
}