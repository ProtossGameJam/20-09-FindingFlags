using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ESQNetwork
{
    public class StageControl : MonoBehaviourPunCallbacks
    {
        #region Photon Callback Method

        /// <summary>
        ///     Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneHandler.LoadScene(SceneType.LOBBY);
        }

        #endregion

        #region Photon Player Method

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

            if (PhotonNetwork.IsMasterClient) {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}",
                    PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                LoadStage();
            }
        }

        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

            if (PhotonNetwork.IsMasterClient) {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}",
                    PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
                LoadStage();
            }
        }

        #endregion
        
        private void LoadStage()
        {
            if (!PhotonNetwork.IsMasterClient)
                Debug.LogError("[ERROR] PhotonNetwork : Trying to Load a level but we are not the master Client");

            print($"[DEBUG] PhotonNetwork : Loading Level - {PhotonNetwork.CurrentRoom.PlayerCount}");
            // TODO: 씬의 이름을 Scene Manager등의 클래스에서 불러오게 하기
            PhotonNetwork.LoadLevel(SceneHandler.GetSceneName(SceneType.STAGE));
        }

        public void LeaveStage()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}