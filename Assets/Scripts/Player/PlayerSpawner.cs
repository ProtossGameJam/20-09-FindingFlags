using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviourPun {
    [Serializable]
    public class SpawnPoint {
        public int index;
        public Transform point;
        public bool isSpawned;

        public SpawnPoint(int index, Transform point) {
            this.index = index;
            this.point = point;
            isSpawned = false;
        }
    }
    
    [SerializeField] private string playerPrefabPath;
    [ReadOnly, SerializeField] private List<SpawnPoint> spawnPoint;
    
    [SerializeField] private CameraAssigner cameraAssigner;

    private void Awake() {
        //InitSpawnPoint();
    }

    private void Start() {
        SpawnPlayer();
    }

    private void InitSpawnPoint() {
        var tempSpawnPoints = GetComponentsInChildren<Transform>();
        if (spawnPoint == null) {
            spawnPoint = new List<SpawnPoint>(tempSpawnPoints.Length);
        }
        for (var i = 0; i < tempSpawnPoints.Length; i++) {
            if (spawnPoint[i] == null) {
                spawnPoint[i] = new SpawnPoint(i, tempSpawnPoints[i]);
            }
        }
    }

    private void SpawnPlayer() {
        print("[DEBUG] Execute : SpawnPlayer()");
        var playerTransform = PhotonNetwork.Instantiate($"{playerPrefabPath}/Player", Vector3.zero, Quaternion.identity).transform;
        MoveToSpawnPoint(playerTransform);
        cameraAssigner.AssignTarget(playerTransform);
    }

    private void MoveToSpawnPoint(Transform player) {
        var spawnIndex = (player.GetComponent<PhotonView>().ViewID / 1000 - 1) % 4;
        print($"[DEBUG] Execute : MoveToSpawnPoint() - Index : {spawnIndex}");
        if (spawnPoint.Exists(pt => pt.index == spawnIndex)) {
            var tempPoint = spawnPoint.Find(pt => pt.index == spawnIndex);
            player.position = tempPoint.point.position;
            photonView.RPC("SetSpawned", RpcTarget.AllBuffered, tempPoint.index);
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