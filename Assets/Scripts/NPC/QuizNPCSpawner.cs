using Photon.Pun;
using UnityEngine;

public class QuizNPCSpawner : MonoSingleton<QuizNPCSpawner>, IPunObservable
{
    [SerializeField] private int npcCount;

    [SerializeField] private string npcPrefabPath;

    [SerializeField] private NPCDataObject[] npcData;
    
    [SerializeField] private Transform[] spawnPoint;

    private void Start()
    {
        if (!PhotonNetwork.LocalPlayer.IsMasterClient) return;
        
        if (npcCount > spawnPoint.Length) {
            Debug.LogError("[ERROR] NPC Count is higher then NPC Spawn Point. Reduced to Spawn Point Count.");
            npcCount = spawnPoint.Length;
        }
        
        ShuffleNPCSpawnPosition(2);
        ShuffleNPCArray(2);
        
        print("[DEBUG] Execute : SpawnNPC()");
        for (var i = 0; i < npcCount; i++) {
            SpawnNPC(i);
        }
    }

    private void SpawnNPC(int index)
    {
        PhotonNetwork.InstantiateRoomObject($"{npcPrefabPath}/NPC", spawnPoint[index].position, Quaternion.identity)
            .GetComponent<NPCManager>().SetData(npcData[index % npcData.Length]);
    }

    private void ShuffleNPCArray(int complexity)
    {
        print("[DEBUG] Execute : ShuffleNPCArray()");
        var tempNpcCount = npcData.Length;
        while (complexity-- > 0) {
            for (var i = 0; i < tempNpcCount; i++) {
                var randNum = Random.Range(0, tempNpcCount);
                var tempObj = npcData[randNum];
                npcData[randNum] = npcData[i];
                npcData[i] = tempObj;
            }   
        }
    }

    private void ShuffleNPCSpawnPosition(int complexity)
    {
        print("[DEBUG] Execute : ShuffleNPCArray()");
        var spawnCount = spawnPoint.Length;
        while (complexity-- > 0) {
            for (var i = 0; i < spawnCount; i++) {
                var randNum = Random.Range(0, spawnCount);
                var tempPoint = spawnPoint[randNum];
                spawnPoint[randNum] = spawnPoint[i];
                spawnPoint[i] = tempPoint;
            }   
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
