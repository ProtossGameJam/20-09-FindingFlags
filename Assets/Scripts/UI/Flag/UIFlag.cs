using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIFlag : MonoBehaviour
{
    [SerializeField] private Image   flagImage;
    [SerializeField] private Image[] edgeImage;

    [ReadOnly] [SerializeField] private FlagColor flagColor;

    public void SetColor(Color flagColor, Color edgeColor) {
        SetColorFadeIn(flagImage, flagColor, Constants.DEFAULT_FADE_TIME);
        SetColorFadeIn(edgeImage, edgeColor, Constants.DEFAULT_FADE_TIME);
    }

    private static void SetColorFadeIn(Image image, Color color, float fadeTime) {
        image.DOColor(color, fadeTime);
        image.DOFade(0.0f, 0.0f).OnComplete(() => image.DOFade(1.0f, fadeTime));
    }

    private static void SetColorFadeIn(Image[] image, Color color, float fadeTime) {
        for (var i = 0; i < image.Length; i++) {
            image[i].DOColor(color, fadeTime);
            image[i].DOFade(0.0f, 0.0f)
                    .OnComplete(() => image[i].DOFade(1.0f, fadeTime));
        }
    }
}