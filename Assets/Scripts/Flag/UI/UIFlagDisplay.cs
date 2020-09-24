using System;
using System.Linq;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[Serializable]
public class FlagColorSetting {
    public Color flagColor;
    public Color edgeColor;
}

public class UIFlagDisplay : MonoBehaviour {
    [SerializeField] private FlagColorDictionary flagColorDic;

    [SerializeField] private float flagOpacity;
    [ReadOnly, SerializeField] private UIFlag[] flagComponents;

    private void Awake() {
        flagComponents = GetComponentsInChildren<UIFlag>();
    }

    public void FlagUIInitialize(FlagColor[] colorArray) {
        for (var i = 0; i < colorArray.Length; i++)
            flagComponents[i].InitFlag(colorArray[i], flagOpacity, flagColorDic[colorArray[i]]);
    }

    public void ShowFlagUI(FlagColor color) {
        foreach (var flag in flagComponents.Where(flag => flag.uiFlagColor == color)) {
            Debug.Log("Find Flag");
            flag.SetFlag(flagColorDic[color]);
        }
    }

    [Serializable]
    public class FlagColorDictionary : SerializableDictionaryBase<FlagColor, Color> { }
}