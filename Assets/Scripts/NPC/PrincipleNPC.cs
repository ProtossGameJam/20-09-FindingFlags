public class PrincipleNPC : NPCBase, IInteractable
{
    public override void SetData(NPCDataObject data)
    {
        base.SetData(data);
    }

    public void FlagCheck()
    {
        
    }

    public void Interact(params object[] param)
    {
        throw new System.NotImplementedException();
    }
}