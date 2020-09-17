using System;
using System.Collections.Generic;
using UnityEngine;

public class UIFlagDisplay : MonoBehaviour
{
    [ReadOnly] [SerializeField] private List<UIFlag> flagList;

    private void Awake() {
        flagList.AddRange(GetComponents<UIFlag>());
    }
    
    // TODO: 깃발 획득 시 나타나도록 UI 구현
}