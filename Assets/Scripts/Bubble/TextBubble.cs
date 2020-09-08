using TMPro;
using UnityEngine;

public class TextBubble : Bubble
{
    [SerializeField] private TextMeshPro bubbleText;
    
    [HideInInspector] public WriteElement writeElement;

    [SerializeField] private float textMaxHorizontalSize;

    public bool IsWriteOver => !writeElement.IsWritingText;

    protected override void Awake()
    {
        base.Awake();
        
        if (bubbleText == null) bubbleText = GetComponentInChildren<TextMeshPro>();
    }

    /// <summary>
    /// 버블에 텍스트를 입력합니다.
    /// </summary>
    /// <param name="text">버블에 표시 할 텍스트</param>
    /// <param name="typeTime">한 글자당 Type 소요 시간</param>
    public void Write(string text, float typeTime)
    {
        BubbleEnabled = true;
        BubbleRectUpdate(text);
        writeElement = TextWriter.AddWriteInstance(bubbleText, text, typeTime, true);
    }

    /// <summary>
    /// 버블의 크기와 위치를 텍스트에 맞게 재조정합니다.
    /// </summary>
    /// <param name="text">버블에 들어갈 텍스트</param>
    private void BubbleRectUpdate(string text)
    {
        bubbleText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textMaxHorizontalSize);

        bubbleText.SetText(text);
        bubbleText.ForceMeshUpdate(true);
        bubbleText.SetText("");

        bubbleSpriteRenderer.size = ResizeBubble(bubbleText.rectTransform, bubbleSpriteRenderer.transform, bubbleText.GetRenderedValues(false), bubblePadding, isReverse);
        transform.localPosition = RepositionBubble(pivotTransform.localPosition, bubbleSpriteRenderer.size, bubbleTailPosFix, isReverse);
    }

    /// <summary>
    /// 버블의 크기를 재조정합니다.
    /// </summary>
    /// <param name="textRect">텍스트의 렉트 컴포넌트</param>
    /// <param name="spriteTransform">버블 이미지의 Transform</param>
    /// <param name="size">버블 안에 들어갈 텍스트의 Rect 크기</param>
    /// <param name="padding">버블의 여백의 크기</param>
    /// <param name="isReverse">버블의 좌우 반전 여부</param>
    private static Vector2 ResizeBubble(RectTransform textRect, Transform spriteTransform, Vector2 size, Vector2 padding, bool isReverse)
    {
        textRect.sizeDelta = size;
        // 버블의 x축 Scale을 음수로 변경해 반대쪽으로 회전
        if (isReverse) {
            var localScale = spriteTransform.localScale;
            localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            spriteTransform.localScale = localScale;
        }
        
        return size + padding;
    }

    /// <summary>
    /// 버블의 위치를 Pivot으로 재조정 합니다.
    /// </summary>
    /// <param name="pivot">버블의 Pivot</param>
    /// <param name="size">버블의 크기</param>
    /// <param name="fixXPos">버블의 꼬리 위치 조정점</param>
    /// <param name="isReverse">버블의 좌우 반전 여부</param>
    /// <returns>Repositioned value</returns>
    private static Vector2 RepositionBubble(Vector2 pivot, Vector2 size, float fixXPos, bool isReverse)
    {
        var xFixPos = isReverse ? -size.x / 2.0f - fixXPos : size.x / 2.0f + fixXPos;
        return new Vector2(xFixPos + pivot.x, size.y / 2.0f + pivot.y);
    }
}