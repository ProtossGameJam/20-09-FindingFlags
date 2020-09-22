using System;
using UnityEngine;

public enum NPCType
{
    Default,
    Master,
    Teacher,
    Child
}

public abstract class NPCBase : MonoBehaviour
{
    [SerializeField] protected NPCType npcType;

    [SerializeField] protected NPCDataObject npcData;
    [SerializeField] protected InteractModule[] interactModule;

    protected virtual void Awake() {
        if (interactModule == null) interactModule = GetComponents<InteractModule>();
    }

    protected void Start() {
        DisableInteract();
    }

    public void EnableInteract() {
        foreach (var module in interactModule) {
            module.IsInteractable = true;
        }
    }
    
    public void DisableInteract() {
        foreach (var module in interactModule) {
            module.IsInteractable = false;
        }
    }

    public virtual void SetData(NPCDataObject data) {
        npcData = data;
    }
}