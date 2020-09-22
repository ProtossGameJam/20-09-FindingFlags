using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class CharacterRenderer : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float renderDistance;

    [ReadOnly] [SerializeField] private List<NPCBase> npcCharacter;

    private void Start() {
        FindNPC();
    }

    private void Update() {
        RenderNPCByDistance(renderDistance);
    }

    private void FindNPC() {
        var temp = FindObjectsOfType<NPCBase>();
        npcCharacter.AddRange(temp);
    }

    private void RenderNPCByDistance(float distance) {
        foreach (var npc in npcCharacter.Where(npc => npc != null))
            if (Vector3.Distance(cameraTransform.position, npc.transform.position) < distance) {
                if (!npc.gameObject.activeSelf) npc.gameObject.SetActive(true);
            }
            else {
                if (npc.gameObject.activeSelf) npc.gameObject.SetActive(false);
            }
    }
}