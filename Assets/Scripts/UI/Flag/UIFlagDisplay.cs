using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[Serializable]
public class FlagColorSetting
{
    public Color flagColor;
    public Color edgeColor;
}

public class UIFlagDisplay : MonoBehaviour
{
    [Serializable]
    public class FlagColorDictionary : SerializableDictionaryBase<FlagColor, FlagColorSetting> { }

    [SerializeField] private FlagColorDictionary flagColorDic;

    [ReadOnly] [SerializeField] private UIFlag[] flagComponents;

    private void Awake() { flagComponents = GetComponentsInChildren<UIFlag>(); }

    public void FlagUIInitialize(FlagColor[] colorArray) {
        for (var i = 0; i < colorArray.Length; i++) {
            var tempSetting = flagColorDic[colorArray[i]];
            flagComponents[i].InitFlag(tempSetting.flagColor, tempSetting.edgeColor);
        }
    }

    public void ShowFlagUI(FlagColor color) {
        
    }

    // TODO: 깃발 획득 시 나타나도록 UI 구현
}