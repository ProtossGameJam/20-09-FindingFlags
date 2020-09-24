using UnityEngine;

public enum NPCType {
    Default, Master, Teacher, Child
}

public abstract class NPCBase : MonoBehaviour {
    [ReadOnly] public NPCType npcType;

    [SerializeField] protected NPCDataObject npcData;
    [SerializeField] protected InteractModule dialogueModule;

    protected virtual void Awake() {
        dialogueModule = GetComponent<DialogueViewer>();
    }

    public virtual void SetData(NPCDataObject data) {
        npcData = data;
    }
}