using TMPro;
using UnityEngine;

public class TextBubble : Bubble
{
    [SerializeField] private TextMeshPro bubbleText;

    [HideInInspector] public WriteElement writeElement;

    [SerializeField] private float textMaxHorizontalSize;

    public bool IsEndWriteLine => !writeElement.IsWritingText;

    protected override void Awake() {
        base.Awake();

        if (bubbleText == null) bubbleText = GetComponentInChildren<TextMeshPro>();
    }

    /// <summary>
    ///     버블에 텍스트를 입력합니다.
    /// </summary>
    /// <param name="text">버블에 표시 할 텍스트</param>
    /// <param name="typeTime">한 글자당 Type 소요 시간</param>
    public void Write(string text, float typeTime) {
        BubbleEnabled = true;

        BubbleRectUpdate(TextRectUpdate(bubbleText, text, textMaxHorizontalSize));

        writeElement = TextWriter.AddWriteInstance(bubbleText, text, typeTime, true);
    }

    /// <summary>
    ///     텍스트 Rect를 버블에 들어갈 Text의 크기에 맞게 재조정합니다.
    /// </summary>
    /// <param name="textComponent">Text Component</param>
    /// <param name="text">버블에 들어갈 Text</param>
    /// <param name="maxHorizontalSize">Text가 가질 수 있는 Max X축 Rect 사이즈</param>
    /// <returns>Text Rect Content 사이즈</returns>
    private static Vector2 TextRectUpdate(TextMeshPro textComponent, string text, float maxHorizontalSize) {
        textComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHorizontalSize);

        textComponent.SetText(text);
        textComponent.ForceMeshUpdate(true);
        textComponent.SetText("");

        var tempSize = textComponent.GetRenderedValues(false);
        textComponent.rectTransform.sizeDelta = tempSize;
        return tempSize;
    }

    /// <summary>
    ///     버블의 스프라이트를 Content에 맞게 크기와 위치를 재조정합니다.
    /// </summary>
    protected override void BubbleRectUpdate(Vector2 contentSize) {
        SetFitSize(bubbleSpriteRenderer, contentSize, bubblePadding, isReversed);
        SetCenterPos(transform, pivotTransform.localPosition, bubbleSpriteRenderer.size, bubbleTailPosFix, isReversed);
    }
}