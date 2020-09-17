using UnityEngine;

public class DefaultNPC : NPCBase, IInteractable
{
    public override void SetData(NPCDataObject data)
    {
        base.SetData(data);
        SetSprite(data.setting);
    }

    private void SetSprite(NPCDataObject.NPCSetting setting)
    {
        // TODO: 인덱스로 NPC 외형 변경하는 스크립트 구현
    }

    public void Interact(params object[] param)
    {
        
    }
}