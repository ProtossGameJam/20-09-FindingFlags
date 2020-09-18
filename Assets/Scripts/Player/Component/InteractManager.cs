using System.Collections;
using Photon.Pun;
using UnityEngine;
// ReSharper disable InconsistentNaming
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable InvertIf

public class InteractManager : MonoBehaviourPun
{
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask npcLayer;

    [ReadOnly] [SerializeField] private bool isInteracting;

    private void Update() {
        if (photonView.IsMine) {
            TryInteractNPC(KeyCode.F);
        }
    }

    private void TryInteractNPC(KeyCode key) {
        InputManager.InputAction(key, CheckNPCAvailable);
    }

    private void CheckNPCAvailable() {
        var castNpc = Physics2D.OverlapCircle(transform.position + Vector3.up, checkRadius, npcLayer);
        if (castNpc) {
            StartInteract(castNpc.transform.GetComponent<NPCBase>());
        }
    }

    private void StartInteract(IInteractable npc) {
        if (!isInteracting) {
            isInteracting = true;
            npc.Interact();
        }
    }

    public void EndInteract() {
        isInteracting = false;
    }
}