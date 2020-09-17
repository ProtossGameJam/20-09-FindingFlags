using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Custom Type/Create NPCData", order = 0)]
public class NPCDataObject : ScriptableObject
{
    public string     name;
    public NPCSetting setting;

    [Serializable]
    public class NPCSetting
    {
        public int skinType;
        public int hairType;
        public int backHairType;
        public int clothType;
        public int eyeType;
    }
}