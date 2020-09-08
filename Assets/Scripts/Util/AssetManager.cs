using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    private static AssetManager _instance;

    public static AssetManager Instance
    {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<AssetManager>();
                if (_instance == null) {
                    _instance = new GameObject("AssetManager").AddComponent<AssetManager>();
                }
            }
            return _instance;
        }
    }
    
    [System.Serializable]
    public class BubbleDictionary : SerializableDictionaryBase<TextBubble.BubbleType, GameObject> { }
    public BubbleDictionary bubbleDic;
}