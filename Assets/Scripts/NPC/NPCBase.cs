using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
    [SerializeField] protected NPCDataObject npcData;
    
    public virtual void SetData(NPCDataObject data)
    {
        npcData = data;
    }
}