using System;
using Photon.Pun;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable ParameterHidesMember

public class NPCSpawner : MonoSingleton<NPCSpawner>, IPunObservable
{
    [SerializeField] private NPCManager manager;
    
    [SerializeField] private int npcCount;

    [SerializeField] private string npcPrefabPath;
    [SerializeField] private string npcPrefabPrefix;

    [SerializeField] private string[] NPCNames;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private DialogueData[] dialogues;
    [SerializeField] private FlagColor[] flags;

    protected override void Awake() {
        base.Awake();

        if (npcCount > spawnPoint.Length) {
            Debug.LogError("[ERROR] NPC Count is higher then NPC Spawn Point. Reduced to Spawn Point Count.");
            npcCount = spawnPoint.Length;
        }

        NPCNames = ShufleUtillity.GetShuffledArray(NPCNames, 2);
        spawnPoint = ShufleUtillity.GetShuffledArray(spawnPoint, 2);
        dialogues = ShufleUtillity.GetShuffledArray(dialogues, 2);
        flags = ShufleUtillity.GetShuffledArray((FlagColor[]) Enum.GetValues(typeof(FlagColor)));
    }

    private void Start() {
        if (!PhotonNetwork.LocalPlayer.IsMasterClient) return;

        print("[DEBUG] Execute : SpawnNPC()");
        for (var i = 0; i < npcCount; i++) SpawnNPC(i % npcCount);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }

    private void SpawnNPC(int index) {
        print($"[DEBUG] Execute : SpawnNPC() - {npcPrefabPath + npcPrefabPrefix + "_" + NPCNames[index]}");
        var npcObj = PhotonNetwork.InstantiateRoomObject(npcPrefabPath + npcPrefabPrefix + "_" + NPCNames[index],
            spawnPoint[index].position, Quaternion.identity);
        var npcComponent = npcObj.GetComponent<DefaultNPC>();
        manager.AddNPC(npcComponent);
        if (npcComponent != null) npcComponent.ownFlag = flags[index % flags.Length];
        npcObj.GetComponent<DialogueViewer>().SetDialogue(dialogues[index % dialogues.Length]);
    }
}