using System.Collections.Generic;
using UnityEngine;

// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class NPCRenderer : MonoBehaviour
{
    [SerializeField] private Camera renderCamera;

    [SerializeField] private float renderDistance;
    [SerializeField] private bool  isEnableCheck = true;

    [ReadOnly] [SerializeField] private List<Transform> npcCharacter;

    private void Start() { FindNPC(); }

    private void Update() { RenderNPCByDistance(renderDistance); }

    private void FindNPC() {
        foreach (var npc in FindObjectsOfType<NPCBase>()) {
            print($"[DEBUG] Execute : FindNPC() - {npc.name}");
            npcCharacter.Add(npc.transform);
        }
    }

    private void RenderNPCByDistance(float distance) {
        if (!isEnableCheck) return;
        foreach (var npc in npcCharacter) {
            npc.gameObject.SetActive(Vector3.Distance(transform.position, npc.position) < distance);
        }
    }
}