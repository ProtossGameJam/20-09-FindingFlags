using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/Dialogue Data Create", order = 0)]
public class DialogueData : ScriptableObject
{
    public int                   id;
    public List<SentenceElement> sentence;

    [Serializable]
    public class SentenceElement
    {
        public                         int    index;
        [Space(10)] [Multiline] public string content;
        [Space(5)]              public string eventCode;
    }
}