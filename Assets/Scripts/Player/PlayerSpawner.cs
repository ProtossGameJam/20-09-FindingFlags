using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[Serializable]
public class SpawnPoint
{
    public int index;
    public Transform point;
    public bool isSpawned;
}

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView pView;

    [SerializeField] private CameraAssigner CameraAssigner;

    [SerializeField] private string playerPrefabPath;

    [SerializeField] private List<SpawnPoint> spawnPoint;

    private void Awake() {
        if (pView == null) pView = GetComponent<PhotonView>();
    }

    private void Start() {
        var playerTransform = SpawnPlayer();
        MoveToSpawnPoint(playerTransform);
        CameraAssigner.AssignTarget(playerTransform);
    }

    private Transform SpawnPlayer() {
        print("[DEBUG] Execute : SpawnPlayer()");
        return PhotonNetwork.Instantiate($"{playerPrefabPath}/Player", Vector3.zero, Quaternion.identity).transform;
    }

    private void MoveToSpawnPoint(Transform player) {
        var spawnIndex = (player.GetComponent<PhotonView>().ViewID / 1000 - 1) % 4;
        print($"[DEBUG] Execute : MoveToSpawnPoint() - Index : {spawnIndex}");
        if (spawnPoint.Exists(pt => pt.index == spawnIndex)) {
            var tempPoint = spawnPoint.Find(pt => pt.index == spawnIndex);
            player.position = tempPoint.point.position;
            pView.RPC("SetSpawned", RpcTarget.AllBuffered, tempPoint.index);
        }
        else {
            print("[ERROR] Execute : GetSpawnPoint() - Not available remain spawn point.");
        }
    }

    [PunRPC]
    private void SetSpawned(int index) {
        print($"[DEBUG] Execute : SetSpawned() - Index : {index}");
        spawnPoint.Find(pt => pt.index == index).isSpawned = true;
    }
}