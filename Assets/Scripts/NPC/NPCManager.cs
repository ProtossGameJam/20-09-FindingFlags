using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class NPCManager : MonoBehaviourPun
{
    [ReadOnly] [SerializeField] private List<DefaultNPC> npcCollection;
    
    [SerializeField] private DialogueData[] dialogues;
    private                  FlagColor[]    flags;

    private void Awake() {
        dialogues = ShufleUtillity.GetShuffledArray(dialogues, 2);
        flags = ShufleUtillity.GetShuffledArray((FlagColor[]) Enum.GetValues(typeof(FlagColor)));
    }
    
    public void NPCSetting() {
        StartCoroutine(NPCSettingRoutine());
    }

    private IEnumerator NPCSettingRoutine() {
        yield return null;
        print("asdasdasdhasfhjkadshfgksdfhgjksdhfjksdhfksdhfjksdhfkjsdhjkf" + FindObjectsOfType<DefaultNPC>().Length);
        npcCollection.AddRange(FindObjectsOfType<DefaultNPC>());
        DialogueAllocToNPC();
        FlagAllocToNPC();
    }

    public void DialogueAllocToNPC() {
        for (var i = 0; i < npcCollection.Count; i++) {
            npcCollection[i].ownFlag = flags[i % flags.Length];
        }
    }

    public void FlagAllocToNPC() {
        for (var i = 0; i < npcCollection.Count; i++) {
            npcCollection[i].GetComponent<DialogueViewer>().SetDialogue(dialogues[i % dialogues.Length]);
        }
    }
}