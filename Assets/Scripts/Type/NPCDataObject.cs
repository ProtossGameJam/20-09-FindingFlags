using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Custom Type/Create NPCData", order = 0)]
public class NPCDataObject : ScriptableObject
{
    public string name;
    public FlagColor ownFlag;
}