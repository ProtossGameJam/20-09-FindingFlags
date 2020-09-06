using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
public class TextContentFitter : UIBehaviour, ILayoutSelfController
{
    public enum FitMode
    {
        // Don't perform any resizing.
        NOT_RESIZE,

        // Resize to the minimum size of the content.
        RESIZE_PERFECT,

        // Resize to the preferred size of the content.
        RESIZE_FLUIDLY
    }
    
    [SerializeField] private FitMode m_HorizontalFit;
    [SerializeField] private FitMode m_VerticalFit;

    [SerializeField] private float m_HorizontalBorder;
    [SerializeField] private float m_VerticalBorder;

    [NonSerialized] private RectTransform m_Rect;

    private DrivenRectTransformTracker m_Tracker;

    public FitMode horizontalFit { get { return m_HorizontalFit; } set { if (!EqualityUtility.IsEqualStruct(ref m_HorizontalFit, value)) SetDirty(); } }
    public FitMode verticalFit { get { return m_VerticalFit; } set { if (!EqualityUtility.IsEqualStruct(ref m_VerticalFit, value)) SetDirty(); } }
    
    private RectTransform rectTransform
    {
        get {
            if (m_Rect == null)
                m_Rect = GetComponent<RectTransform>();
            return m_Rect;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetDirty();
    }

    protected override void OnDisable()
    {
        m_Tracker.Clear();
        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        base.OnDisable();
    }

    protected override void OnRectTransformDimensionsChange()
    {
        SetDirty();
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        SetDirty();
    }
#endif

    public void SetLayoutHorizontal()
    {
        m_Tracker.Clear();
        HandleSelfFittingAlongAxis(0);
    }

    public void SetLayoutVertical()
    {
        HandleSelfFittingAlongAxis(1);
    }

    private void HandleSelfFittingAlongAxis(int axis)
    {
        var fitting = axis == 0 ? horizontalFit : verticalFit;
        if (fitting == FitMode.NOT_RESIZE) {
            // Keep a reference to the tracked transform, but don't control its properties:
            m_Tracker.Add(this, rectTransform, DrivenTransformProperties.None);
            return;
        }

        m_Tracker.Add(this, rectTransform,
            axis == 0 ? DrivenTransformProperties.SizeDeltaX : DrivenTransformProperties.SizeDeltaY);

        // Set size to min or preferred size
        if (fitting == FitMode.RESIZE_PERFECT)
            rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis) axis,
                LayoutUtility.GetPreferredSize(m_Rect, axis));
        else
            rectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis) axis, LayoutUtility.GetPreferredSize(m_Rect, axis) + (axis == 0 ? m_HorizontalBorder : m_VerticalBorder));
    }

    private void SetDirty()
    {
        if (!IsActive())
            return;

        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
    }
}