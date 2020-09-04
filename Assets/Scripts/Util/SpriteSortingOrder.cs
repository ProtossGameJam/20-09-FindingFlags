using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SpriteSortingOrder : MonoBehaviour
{
    [ReadOnly] [SerializeField] private new Renderer renderer;

    [ReadOnly] [SerializeField] private int orderFix;
    [ReadOnly] [SerializeField] private int baseOrder;
    [SerializeField] private int orderOffset;

    private void Reset()
    {
        orderFix = 100;
        baseOrder = 1000;
    }

    private void Awake()
    {
        if (renderer == null) {
            renderer = GetComponent<Renderer>();
        }
    }

    private void Start()
    {
        SortingOrderFix();
    }

    public void SortingOrderFix()
    {
        renderer.sortingOrder = (int) (baseOrder + transform.position.y * orderFix + orderOffset);
    }
}
