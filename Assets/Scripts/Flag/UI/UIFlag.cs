using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIFlag : MonoBehaviour
{
    [SerializeField] private Image flagImage;
    [SerializeField] private Image[] edgeImage;

    [ReadOnly] public FlagColor uiFlagColor;

    public void InitFlag(FlagColor color, FlagColorSetting setting) {
        uiFlagColor = color;
        TweenColorFadeIn(flagImage, setting.flagColor, 0.5f, 0.0f);
        for (var i = 0; i < 2; i++) TweenColorFadeIn(edgeImage[i], setting.edgeColor, 0.5f, 0.0f);
    }

    public void SetFlag(FlagColorSetting setting) {
        Debug.Log("Set Flag");
        TweenColorFadeIn(flagImage, setting.flagColor, 1.0f, Constants.DEFAULT_FADE_TIME);
        for (var i = 0; i < 2; i++)
            TweenColorFadeIn(edgeImage[i], setting.edgeColor, 1.0f, Constants.DEFAULT_FADE_TIME);
    }

    private static void TweenColor(Image image, Color color, float time) {
        image.DOColor(color, time);
    }

    private static void TweenColorFadeIn(Image image, Color color, float transparent, float time) {
        Debug.Log("Tween Flag Color");
        image.DOColor(color, time);
        image.DOFade(transparent, time);
    }
}