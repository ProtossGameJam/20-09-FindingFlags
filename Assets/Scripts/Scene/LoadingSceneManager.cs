using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static string NextSceneName;

    private void Start() { StartCoroutine(LoadScene()); }

    private static IEnumerator LoadScene() {
        yield return null;

        var op = SceneManager.LoadSceneAsync(NextSceneName);
        op.allowSceneActivation = false;

        while (!op.isDone) {
            yield return null;
            if (op.progress >= 0.9f) {
                yield return new WaitForSeconds(0.8f); //DEBUG
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}