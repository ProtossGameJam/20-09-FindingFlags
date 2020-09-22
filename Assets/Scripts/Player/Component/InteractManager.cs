using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable Unity.PerformanceCriticalCodeInvocation
// ReSharper disable InvertIf

public class InteractManager : MonoBehaviourPun
{
    [SerializeField] private float     checkRadius;
    [SerializeField] private LayerMask npcLayer;

    [ReadOnly] [SerializeField] private bool enableInteract = false;

    private void Update() {
        if (photonView.IsMine) TryInteractNPC(KeyCode.F);
    }

    public void SetEnableInteract(bool enable) {
        enableInteract = enable;
    }

    private void TryInteractNPC(KeyCode key) { InputManager.InputAction(key, CheckNPCAvailable); }

    private void CheckNPCAvailable() {
        var castNpc = Physics2D.OverlapCircle(transform.position + Vector3.up, checkRadius, npcLayer);
        if (castNpc) DoInteract(castNpc.transform.GetComponents<InteractModule>());
    }

    private void DoInteract(IEnumerable<InteractModule> modules) {
        if (enableInteract)
            foreach (var module in modules)
                module.Interact();
    }
}