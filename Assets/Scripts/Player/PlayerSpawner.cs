using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

public enum PlayerType
{
    NETWORK_SELF, NETWORK_OPPONENT, LOCAL_SELF, LOCAL_OPPONENT, COM_AI
}

public enum PlayerColor
{
    RED, YELLOW, BLUE, GREEN, ORANGE
}

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObjectDictionary<PlayerType> playerObjDic;

    [SerializeField] private List<Transform> spawnPoint;

    [SerializeField] private CinemachineVirtualCamera targetCamera;
    
    private void Start()
    {
        for (var i = 0; i < spawnPoint.Count; i++) {
            if (i != 0) {
                SpawnPlayerObject(PlayerType.NETWORK_OPPONENT, spawnPoint[i]);
            }
            else {
                var character = SpawnPlayerObject(PlayerType.NETWORK_SELF, spawnPoint[i]);
                SetCameraTarget(character);
            }
        }
    }

    public Transform SpawnPlayerObject(PlayerType type, Transform spawnPos)
    {
        return playerObjDic.Instantiate(type, spawnPos.position).transform;
    }

    public void SetCameraTarget(Transform target)
    {
        targetCamera.Follow = target;
        targetCamera.LookAt = target;
    }
}
