using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable ParameterHidesMember

public class NPCSpawner : MonoBehaviour {
    [ReadOnly, SerializeField] private List<DefaultNPC> npcCollection;

    [SerializeField] private int npcCount;

    [SerializeField] private string npcPrefabPath;
    [SerializeField] private string npcPrefabPrefix;

    [SerializeField] private string[] NPCNames;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private DialogueData[] dialogues;
    private FlagColor[] flags;

    private void Awake() {
        if (npcCount > spawnPoint.Length) {
            Debug.LogError("[ERROR] NPC Count is higher then NPC Spawn Point. Reduced to Spawn Point Count.");
            npcCount = spawnPoint.Length;
        }

        NPCNames = ShuffleUtility<string>.GetShuffledArray(NPCNames, 2);
        spawnPoint = ShuffleUtility<Transform>.GetShuffledArray(spawnPoint, 2);
        dialogues = ShuffleUtility<DialogueData>.GetShuffledArray(dialogues, 2);
        flags = ShuffleUtility<FlagColor>.GetShuffledArray((FlagColor[]) Enum.GetValues(typeof(FlagColor)));
    }

    private void Start() {
        if (!PhotonNetwork.LocalPlayer.IsMasterClient) return;

        print("[DEBUG] Execute : SpawnNPC()");
        for (var i = 0; i < npcCount; i++) SpawnNPC(i % npcCount);
    }

    private void SpawnNPC(int index) {
        print($"[DEBUG] Execute : SpawnNPC() - {npcPrefabPath + npcPrefabPrefix + "_" + NPCNames[index]}");
        PhotonNetwork.InstantiateRoomObject(
                npcPrefabPath + npcPrefabPrefix + "_" + NPCNames[index],
                spawnPoint[index].position,
                Quaternion.identity
        );
    }

    public void NPCDefaultSetting() {
        npcCollection.AddRange(FindObjectsOfType<DefaultNPC>());

        for (var i = 0; i < npcCollection.Count; i++) {
            npcCollection[i].ownFlag = flags[i % flags.Length];
            npcCollection[i].GetComponent<DialogueViewer>().SetDialogue(dialogues[i % dialogues.Length]);
        }
    }
}