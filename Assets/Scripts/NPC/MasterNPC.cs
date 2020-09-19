public class MasterNPC : NPCBase
{
    protected override void Awake() {
        base.Awake();
        npcType = NPCType.Master;
    }

    public override void SetData(NPCDataObject data) { base.SetData(data); }

    public void FlagCheck() { }

    public override void Interact(params object[] param) { interactModule.ActiveModuleObject(); }
}