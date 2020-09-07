using UnityEngine;
using TMPro;

public class TextBubble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bubbleSpriteRenderer;
    [SerializeField] private TextMeshPro bubbleText;

    private Vector2 bubbleSpawnPosition;
    
    [SerializeField] private Vector2 bubbleFixPosition;

    [SerializeField] private float textMaxHorizontalSize;
    
    private Vector2 bubbleSpritePadding;
    [SerializeField] private float bubbleHorizontalPadding;
    [SerializeField] private float bubbleVerticalPadding;

    private void Awake()
    {
        if (bubbleSpriteRenderer == null) {
            bubbleSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (bubbleText == null) {
            bubbleText = GetComponentInChildren<TextMeshPro>();
        }
        
        bubbleSpritePadding = new Vector2(bubbleHorizontalPadding, bubbleVerticalPadding);
    }

    public static TextBubble CreateBubble(Transform parent)
    {
        // TODO: Disable on create
        var instance = Instantiate(AssetManager.Instance.ChatBubble, parent).GetComponent<TextBubble>();
        instance.SetSpawnPoint(parent.position);
        return instance;
    }

    /// <summary>
    /// Bubble Text Setup
    /// </summary>
    /// <param name="text">Present text in bubble</param>
    private void Write(string text)
    {
        // TODO: 말풍선 반대 방향으로도 출력하는 기능 구현
        bubbleText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textMaxHorizontalSize);
        
        bubbleText.SetText(text);
        bubbleText.ForceMeshUpdate();
        BubbleResize(bubbleText.GetRenderedValues(false));
        transform.position = BubbleMoveBySize(bubbleSpriteRenderer.size);
        
        TextWriter.WriteSingle(bubbleText, text, 0.1f);
    }

    /// <summary>
    /// Set bubble spawn position
    /// </summary>
    /// <param name="point">parent's position</param>
    public void SetSpawnPoint(Vector2 point)
    {
        bubbleSpawnPosition = point;
    }

    /// <summary>
    /// Bubble Sprite & Text Rect Resize
    /// </summary>
    /// <param name="size">Text Mesh's size</param>
    private void BubbleResize(Vector2 size)
    {
        bubbleText.rectTransform.sizeDelta = size;
        
        EqualityUtility.IsEqualStruct(ref bubbleSpritePadding,
            new Vector2(bubbleHorizontalPadding, bubbleVerticalPadding));
        bubbleSpriteRenderer.size = size + bubbleSpritePadding;
    }

    /// <summary>
    /// Move bubble sprite at specific position
    /// </summary>
    /// <param name="position">Bubble target position</param>
    /// <param name="size">Bubble sprite size</param>
    /// <returns>Repositioned value</returns>
    private Vector2 BubbleMoveBySize(Vector2 size)
    {
        return new Vector2(size.x / 2.0f + bubbleFixPosition.x + bubbleSpawnPosition.x, size.y / 2.0f + bubbleFixPosition.y + bubbleSpawnPosition.y);
    }
}
