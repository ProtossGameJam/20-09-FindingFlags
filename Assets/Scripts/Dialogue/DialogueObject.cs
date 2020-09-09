using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue Element Create", order = 0)]
public class DialogueObject : ScriptableObject
{
    [System.Serializable]
    public class DialogueElement
    {
        public int group;
        public List<SentenceElement> sentence;
    }

    [System.Serializable]
    public class SentenceElement
    {
        public int index;
        [Multiline]
        public string content;
        public string eventCode;
    }

    public List<DialogueElement> data;
}