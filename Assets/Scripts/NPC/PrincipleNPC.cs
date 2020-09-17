using System;

public class PrincipleNPC : NPCBase, IInteractable
{
    public void Interact(params object[] param) { throw new NotImplementedException(); }

    public override void SetData(NPCDataObject data) { base.SetData(data); }

    public void FlagCheck() { }
}