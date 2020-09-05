using System;
using Cinemachine;
using FlagGame;
using UnityEngine;

public class MultiPlayerHandler : MonoBehaviour
{
    [SerializeField] private MultiPlayerStorage playerStorage;
    [SerializeField] private CharacterSpawner spawnPoint;
    
    [Header("플레이어 입장 시 기본설정")]
    [SerializeField] private CinemachineVirtualCamera targetCamera;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        if (playerStorage == null) {
            playerStorage = GetComponent<MultiPlayerStorage>();
        }
    }

    public void EnterPlayer(bool isPlayable = false)
    {
        print(playerStorage.IsPlayerFull);
        if (playerStorage.IsPlayerFull) return;

        var tempPlayer = spawnPoint.SpawnCharacter(playerStorage.GetRemainColor);
        if (isPlayable) {
            SetMainCharacter(tempPlayer);
        }
        playerStorage.AddPlayer(tempPlayer);
    }

    private void SetMainCharacter(CharacterManager player)
    {
        var tempTransform = player.transform;
        SetInput(tempTransform);
        SetMovement(tempTransform);
        SetCameraTarget(tempTransform);
    }

    private void SetInput(Transform target)
    {
        target.gameObject.AddComponent<PlayerInput>();
    }

    private void SetMovement(Transform target)
    {
        var tempComp = target.gameObject.AddComponent<PlayerMove>();
        tempComp.SetMoveSpeed(moveSpeed);
    }
    
    private void SetCameraTarget(Transform target)
    {
        targetCamera.Follow = target;
        targetCamera.LookAt = target;
    }
}