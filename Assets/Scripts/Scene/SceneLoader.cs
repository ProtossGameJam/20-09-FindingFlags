using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Scene Load Method

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public static void LoadScene(SceneType scene)
    {
        SceneManager.LoadScene(SceneData.GetSceneName(scene));
    }
    public static void LoadSceneAsync(string scene)
    {
        LoadingSceneManager.NextSceneName = scene;
        SceneManager.LoadScene(SceneData.GetSceneName(SceneType.LOADING));
    }
    public static void LoadSceneAsync(SceneType scene)
    {
        LoadingSceneManager.NextSceneName = SceneData.GetSceneName(scene);
        SceneManager.LoadScene(SceneData.GetSceneName(SceneType.LOADING));
    }

    #endregion
}