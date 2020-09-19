using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIFlag : MonoBehaviour
{
    [SerializeField] private Image   flagImage;
    [SerializeField] private Image[] edgeImage;

    [ReadOnly] [SerializeField] private FlagColor flagColor;

    public void InitFlag(Color flagColor, Color edgeColor) {
        TweenColorFadeIn(flagImage, flagColor, 0.5f, 0.0f);
        for (var i = 0; i < 2; i++) {
            TweenColorFadeIn(edgeImage[i], edgeColor, 0.5f, 0.0f);
        }
    }

    public void SetFlag(Color flagColor, Color edgeColor) {
        TweenColorFadeIn(flagImage, flagColor, 1.0f, Constants.DEFAULT_FADE_TIME);
        for (var i = 0; i < 2; i++) {
            TweenColorFadeIn(edgeImage[i], edgeColor, 1.0f, Constants.DEFAULT_FADE_TIME);
        }
    }

    private static void TweenColor(Image image, Color color, float time) {
        image.DOColor(color, time);
    }
    
    private static void TweenColorFadeIn(Image image, Color color, float transparent, float time) {
        image.DOColor(color, time);
        image.DOFade(1.0f, time);
    }
}