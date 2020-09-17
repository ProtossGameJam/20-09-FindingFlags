using UnityEngine;

public class NPCManager : MonoBehaviour, INPCEssential
{
    [SerializeField] protected NPCDataObject npcData;
    
    public virtual void SetData(NPCDataObject data)
    {
        npcData = data;
        SetSprite(data.setting);
    }

    private void SetSprite(NPCDataObject.NPCSetting setting)
    {
        // TODO: 인덱스로 NPC 외형 변경하는 스크립트 구현
    }
}