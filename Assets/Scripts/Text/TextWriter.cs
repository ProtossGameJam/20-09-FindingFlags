using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    private static TextWriter _instance;
    
    private List<WriteElement> _writeTextList;

    private void Awake()
    {
        _instance = this;
        
        _writeTextList = new List<WriteElement>();
    }

    private void Update()
    {
        for (var i = 0; i < _writeTextList.Count; i++) {
            if (!_writeTextList[i].WriteText()) continue; // Check text writing is end
            _writeTextList.RemoveAt(i--); // Remove write instance
        }
    }
    
    public static WriteElement AddWriteInstance(TextMeshPro textComponent, string text, float charTime, bool removeRedundant)
    {
        if (removeRedundant) _instance.RemoveWriter(textComponent);
        return _instance.AddWriter(textComponent, text, charTime);
    }
    
    private WriteElement AddWriter(TextMeshPro textComponent, string text, float charTime)
    {
        var writeInstance = new WriteElement(textComponent, text, charTime, null);
        _writeTextList.Add(writeInstance);
        return writeInstance;
    }

    public static void RemoveWriteInstance(TextMeshPro textComponent)
    {
        _instance.RemoveWriter(textComponent);
    }
    
    private void RemoveWriter(TextMeshPro textComponent)
    {
        for (var i = 0; i < _writeTextList.Count; i++) {
            if (_writeTextList[i].textComponent.Equals(textComponent)) {
                _writeTextList.Remove(_writeTextList[i--]);
            }
        }
    }
}