using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [ReadOnly] [SerializeField] private List<NPCBase> npcCollection;

    public void AddNPC(NPCBase npc) {
        npcCollection.Add(npc);
    }

    public void EnableNPCInteract() {
        foreach (var npc in npcCollection) {
            npc.EnableInteract();
        }
    }
}