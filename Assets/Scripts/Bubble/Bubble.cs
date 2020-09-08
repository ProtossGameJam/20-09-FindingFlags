using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] protected Transform pivotTransform;
    [SerializeField] protected SpriteRenderer bubbleSpriteRenderer;
    
    [SerializeField] protected Vector2 bubblePadding; // 0.6 0.8
    [SerializeField] protected float bubbleTailPosFix;

    [SerializeField] protected bool isReverse;

    public bool BubbleEnabled
    {
        get => gameObject.activeSelf;
        set => gameObject.SetActive(value);
    }

    protected virtual void Awake()
    {
        if (bubbleSpriteRenderer == null) bubbleSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        BubbleEnabled = false;
    }
    
    protected virtual void BubbleRectUpdate()
    {
        
    }
    
    //protected virtual Vector2 ResizeBubble()
    //{
    //    
    //}
    
    //protected virtual Vector2 RepositionBubble()
    //{
    //    
    //}
}
