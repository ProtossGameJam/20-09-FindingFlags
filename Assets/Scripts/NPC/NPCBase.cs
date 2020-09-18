using UnityEngine;

public enum NPCType
{
    Default, Principle
}

public abstract class NPCBase : MonoBehaviour, IInteractable
{
    [SerializeField] protected NPCType npcType;
        
    [SerializeField] protected NPCDataObject npcData;
    [SerializeField] protected InteractModule interactModule;

    protected virtual void Awake() {
        if (interactModule == null) {
            interactModule = GetComponent<InteractModule>();
        }
    }
    
    public virtual void SetData(NPCDataObject data) { npcData = data; }

    public abstract void Interact(params object[] param);
}