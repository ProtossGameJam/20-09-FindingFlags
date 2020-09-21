public class DefaultNPC : NPCBase
{
    protected override void Awake() {
        base.Awake();
        npcType = NPCType.Default;
    }
}