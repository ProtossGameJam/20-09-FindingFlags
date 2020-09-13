using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace ESQNetwork
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serialized Variables

        [Tooltip("Lobby의 UI 오브젝트를 관리하는 Manager 스크립트")]
        [SerializeField] private LobbyUIManager lobbyUIManager;
        
        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>
        [Tooltip("하나의 Room에 입장 가능한 플레이어의 최대 인원")]
        [SerializeField] private byte _maxPlayersPerRoom;

        #endregion

        #region Private Variables

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        private const string GameVersion = "1";

        #endregion

        private void Awake()
        {
            // WARNING: This makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        /// <summary>
        /// Start the connection process to "Photon Cloud".
        /// <para> - If already connected, we attempt joining a random room</para>
        /// <para> - If not yet connected, Connect this application instance to Photon Cloud Network</para>
        /// </summary>
        public void Connect()
        {
            lobbyUIManager.OnConnectUI();
            
            // We check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (PhotonNetwork.IsConnected) {
                // WARNING: We need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                PhotonNetwork.JoinRandomRoom();
            }
            else {
                // WARNING: We must first and foremost connect to Photon Online Server.
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        
        #region Photon Callback Method

        public override void OnConnectedToMaster()
        {
            // WARNING: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            lobbyUIManager.OnDisconnectUI();
        }

        public override void OnJoinedRoom()
        {
            // WARNING: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the 'Room for 1' ");
                
                // WARNING: Load the Room Level.
                PhotonNetwork.LoadLevel(SceneHandler.GetSceneName(SceneType.STAGE));
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            // WARNING: We failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = _maxPlayersPerRoom });
        }
        
        #endregion
    }
}
