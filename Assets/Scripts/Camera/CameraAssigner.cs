using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraAssigner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [SerializeField] private Transform localPlayer;

    public void AssignTarget(Transform player) {
        if (player.GetComponent<PhotonView>().IsMine) {
            virtualCamera.Follow = player;
            virtualCamera.LookAt = player;
            localPlayer = player;
        }
    }

    //아.. 어떻게 정의해서 드리면 좋을까..
    public void AssignTargetToUI(Transform ui) {
        virtualCamera.Follow = ui;
        virtualCamera.LookAt = ui;
    }

    public void AssignTargetToPlayer() {
        virtualCamera.Follow = localPlayer;
        virtualCamera.LookAt = localPlayer;
    }
}