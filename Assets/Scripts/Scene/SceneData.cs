using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public enum SceneType
{
    INTRO, LOBBY, STAGE, LOADING
}

public class SceneData : MonoSingleton<SceneData>
{
    [System.Serializable]
    private class SceneDictionary : SerializableDictionaryBase<SceneType, string> { }

    [SerializeField] private SceneDictionary sceneDic;
    
    protected override void Awake()
    {
        base.Awake();
        
        DontDestroyOnLoad(gameObject);
    }

    public static string GetSceneName(SceneType type)
    {
        return Instance.sceneDic[type];
    }
}