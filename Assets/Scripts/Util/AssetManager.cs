using UnityEngine;

public class AssetManager : MonoBehaviour
{
    private static AssetManager _instance;

    public static AssetManager Instance
    {
        get {
            if (_instance == null) {
                _instance = new GameObject("AssetManager").AddComponent<AssetManager>();
            }
            return _instance;
        }
    }

    public GameObject ChatBubble;
}