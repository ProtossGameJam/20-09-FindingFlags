using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBubble : MonoBehaviour
{
    public enum BubbleType
    {
        DEFAULT
    }
    
    [SerializeField] private SpriteRenderer bubbleSpriteRenderer;
    [SerializeField] private TextMeshPro bubbleText;

    [SerializeField] private Vector2 bubbleFixPosition;

    [SerializeField] private float textMaxHorizontalSize;
    [SerializeField] private float bubbleHorizontalPadding;
    [SerializeField] private float bubbleVerticalPadding;

    public bool isReverse;

    private Vector2 bubbleSpritePadding;
    private Transform spawnTransform;

    public void ActiveBubble(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void Initialize()
    {
        if (bubbleSpriteRenderer == null) bubbleSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (bubbleText == null) bubbleText = GetComponentInChildren<TextMeshPro>();

        bubbleSpritePadding = new Vector2(bubbleHorizontalPadding, bubbleVerticalPadding);
    }

    /// <summary>
    /// Create Text Bubble Object
    /// </summary>
    /// <param name="type">Bubble object's type</param>
    /// <param name="parent">Set Object's parent</param>
    /// <param name="isReverse">Select is left side or right side</param>
    /// <returns>Text Bubble Instance</returns>
    public static TextBubble CreateBubble(BubbleType type, Transform parent, bool isReverse = false)
    {
        // TODO: Disable on create
        var instance = Instantiate(AssetManager.Instance.bubbleDic[type], parent).GetComponent<TextBubble>();
        
        instance.Initialize();
        instance.ActiveBubble(false);
        
        instance.spawnTransform = parent;
        instance.isReverse = isReverse;
        
        return instance;
    }

    /// <summary>
    ///     Bubble Text Setup
    /// </summary>
    /// <param name="text">Present text in bubble</param>
    public void Write(string text)
    {
        BubbleUpdate(text);
        TextWriter.WriteSingle(bubbleText, text, 0.1f);
    }

    private void BubbleUpdate(string text)
    {
        ActiveBubble(false);

        // TODO: 말풍선 반대 방향으로도 출력하는 기능 구현
        bubbleText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textMaxHorizontalSize);

        bubbleText.SetText(text);
        bubbleText.ForceMeshUpdate(true);

        BubbleResize(bubbleText.GetRenderedValues(false));
        transform.position = BubbleMoveBySize(bubbleSpriteRenderer.size, bubbleFixPosition.x, bubbleFixPosition.y);

        bubbleText.SetText("");
        bubbleText.ForceMeshUpdate(true);

        ActiveBubble(true);
    }

    /// <summary>
    ///     Bubble Sprite & Text Rect Resize
    /// </summary>
    /// <param name="size">Text Mesh's size</param>
    private void BubbleResize(Vector2 size)
    {
        bubbleText.rectTransform.sizeDelta = size;

        EqualityUtility.IsEqualStruct(ref bubbleSpritePadding,
            new Vector2(bubbleHorizontalPadding, bubbleVerticalPadding));
        bubbleSpriteRenderer.size = size + bubbleSpritePadding;

        if (!isReverse) return;
        
        var spriteTransform = bubbleSpriteRenderer.transform;
        var localScale = spriteTransform.localScale;
        
        localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        spriteTransform.localScale = localScale;
    }

    /// <summary>
    ///     Move bubble sprite at specific position
    /// </summary>
    /// <param name="size">Bubble sprite size</param>
    /// <param name="fixXPos">Bubble X spawn position fixer</param>
    /// <param name="fixYPos">Bubble Y spawn position fixer</param>
    /// <returns>Repositioned value</returns>
    private Vector2 BubbleMoveBySize(Vector2 size, float fixXPos, float fixYPos)
    {
        var targetPos = spawnTransform.position;
        var xFixPos = isReverse ? -size.x / 2.0f - fixXPos : size.x / 2.0f + fixXPos;
        return new Vector2(xFixPos + targetPos.x, size.y / 2.0f + fixYPos + targetPos.y);
    }
}