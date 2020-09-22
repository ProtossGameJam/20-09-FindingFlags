public class DefaultNPC : NPCBase
{
    [ReadOnly] public FlagColor ownFlag;

    protected override void Awake() {
        base.Awake();
        npcType = NPCType.Default;
    }
}