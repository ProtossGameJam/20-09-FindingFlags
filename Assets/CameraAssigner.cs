using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraAssigner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public void AssignTarget(Transform player)
    {
        if (player.GetComponent<PhotonView>().IsMine) {
            virtualCamera.Follow = player;
            virtualCamera.LookAt = player;
        }
    }
}
