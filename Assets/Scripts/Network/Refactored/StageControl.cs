using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ESQNetwork
{
    public class StageControl : MonoBehaviourPunCallbacks
    {
        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            // TODO: GameSceneManager로 관리, Loading 구현
            SceneManager.LoadScene(0);
        }
        
        private void LoadStage()
        {
            if (!PhotonNetwork.IsMasterClient) {
                Debug.LogError("[ERROR] PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            
            print($"[DEBUG] PhotonNetwork : Loading Level - {PhotonNetwork.CurrentRoom.PlayerCount}");
            // TODO: 씬의 이름을 Scene Manager등의 클래스에서 불러오게 하기
            PhotonNetwork.LoadLevel("TempStage");
        }
        
        public void LeaveStage()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}