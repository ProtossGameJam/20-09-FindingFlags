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
    [ReadOnly] public NPCType npcType;

    [SerializeField] protected NPCDataObject    npcData;
    [SerializeField] protected InteractModule[] interactModule;

    protected virtual void Awake() {
        interactModule = GetComponents<InteractModule>();
    }

    public virtual void SetData(NPCDataObject data) { npcData = data; }
}