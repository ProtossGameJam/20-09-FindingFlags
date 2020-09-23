using UnityEngine;

public class Bubble : MonoBehaviour {
    [SerializeField] protected Transform pivotTransform;

    [SerializeField] protected SpriteRenderer bubbleSpriteRenderer;

    [SerializeField] protected Vector2 bubblePadding; // 0.6 0.8
    [SerializeField] protected float bubbleTailPosFix;

    [SerializeField] protected bool isReversed;

    public bool BubbleEnabled { get => gameObject.activeSelf; set => gameObject.SetActive(value); }

    protected virtual void Awake() {
        if (bubbleSpriteRenderer == null) bubbleSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void BubbleRectUpdate(Vector2 contentSize) { }

    protected static void SetFitSize(SpriteRenderer bubbleRenderer
                                   , Vector2        contentSize
                                   , Vector2        padding
                                   , bool           isReversed) {
        bubbleRenderer.size = contentSize + padding;
        bubbleRenderer.flipX = isReversed;
    }

    protected static void SetCenterPos(Transform bubbleTransform
                                     , Vector2   pivot
                                     , Vector2   bubbleSize
                                     , float     tailFixPos
                                     , bool      isReversed) {
        var xFixPos = isReversed ? -bubbleSize.x / 2.0f - tailFixPos : bubbleSize.x / 2.0f + tailFixPos;
        bubbleTransform.localPosition = new Vector2(xFixPos + pivot.x, bubbleSize.y / 2.0f + pivot.y);
    }
}