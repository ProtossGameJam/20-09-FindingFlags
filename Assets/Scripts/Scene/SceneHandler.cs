using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    INTRO, LOBBY, STAGE, LOADING
}

public class SceneHandler : MonoSingleton<SceneHandler>
{
    [System.Serializable]
    private class SceneDictionary : SerializableDictionaryBase<SceneType, string> { }

    [SerializeField] private SceneDictionary sceneDic;

    #region Scene Load Method

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public static void LoadScene(SceneType scene)
    {
        SceneManager.LoadScene(GetSceneName(scene));
    }
    public static void LoadSceneAsync(string scene)
    {
        LoadingSceneManager.NextSceneName = scene;
        SceneManager.LoadScene(GetSceneName(SceneType.LOADING));
    }
    public static void LoadSceneAsync(SceneType scene)
    {
        LoadingSceneManager.NextSceneName = GetSceneName(scene);
        SceneManager.LoadScene(GetSceneName(SceneType.LOADING));
    }

    #endregion

    public static string GetSceneName(SceneType type)
    {
        return Instance.sceneDic[type];
    }
}