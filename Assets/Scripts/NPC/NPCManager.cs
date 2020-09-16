using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private NPCDataObject npcData;
    
    public void SetNPC(NPCDataObject data)
    {
        npcData = data;
        
        // TODO: 인덱스로 NPC 외형 변경하는 스크립트 구현
        SetSprite(data.setting);
    }

    private void SetSprite(NPCDataObject.NPCSetting setting)
    {
        
    }
}