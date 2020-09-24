using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraAssigner : MonoBehaviour {
    [ReadOnly, SerializeField] private CinemachineVirtualCamera virtualCamera;

    [ReadOnly, SerializeField] private Transform playerTarget;

    private void Awake() {
        if (virtualCamera == null) {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
    }

    public void AssignTarget(Transform player) {
        if (player.GetComponent<PhotonView>().IsMine) {
            virtualCamera.Follow = player;
            virtualCamera.LookAt = player;
            playerTarget = player;
        }
    }

    public void AssignTargetToUI(Transform uiTarget) {
        virtualCamera.Follow = uiTarget;
        virtualCamera.LookAt = uiTarget;
    }

    public void AssignTargetToPlayer() {
        virtualCamera.Follow = playerTarget;
        virtualCamera.LookAt = playerTarget;
    }
}