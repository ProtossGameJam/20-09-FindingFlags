using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TextBubbleResizer : MonoBehaviour
{
    [SerializeField] private RectTransform bubbleRect;
    [SerializeField] private RectTransform textRect;

    [SerializeField] private float m_HorizontalBorder;
    [SerializeField] private float m_VerticalBorder;
    
    [SerializeField] private bool isUpdate;

    private void Awake()
    {
        if (bubbleRect == null) {
            bubbleRect = GetComponent<RectTransform>();
        }

        if (textRect == null) {
            textRect = GetComponentInChildren<RectTransform>();
        }
    }

    private TMP_Text text;
    private void Start()
    {
        SetBubbleSize(textRect.sizeDelta);
    }

    private void Update()
    {
        if (isUpdate) {
            SetBubbleSize(textRect.sizeDelta);
        }
    }

    private void SetBubbleSize(Vector2 textSize)
    {
        bubbleRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textSize.x);
        bubbleRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textSize.y);
    }
}