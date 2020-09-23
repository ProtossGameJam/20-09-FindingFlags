using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIFlag : MonoBehaviour {
    [SerializeField] private Image flagImage;
    [SerializeField] private Image[] edgeImage;

    [ReadOnly] public FlagColor uiFlagColor;

    public void InitFlag(FlagColor flag, float opacity, Color color) {
        uiFlagColor = flag;
        TweenColorFadeIn(flagImage, color, opacity, 0.0f);
    }

    public void SetFlag(Color color) {
        Debug.Log("Set Flag");
        TweenColorFadeIn(flagImage, color, 1.0f, Constants.DEFAULT_FADE_TIME);
    }

    private static void TweenColor(Image image, Color color, float time) => image.DOColor(color, time);

    private static void TweenColorFadeIn(Image image, Color color, float transparent, float time) {
        Debug.Log("Tween Flag Color");
        image.DOColor(color, time);
        image.DOFade(transparent, time);
    }
}