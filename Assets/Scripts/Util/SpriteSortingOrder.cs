using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SpriteSortingOrder : MonoBehaviour
{
    [SerializeField] protected Renderer _renderer;

    [ReadOnly] [SerializeField] protected int orderFix;
    [ReadOnly] [SerializeField] protected int baseOrder;
    [SerializeField] protected int orderOffset;

    protected virtual void Reset()
    {
        orderFix = 100;
        baseOrder = 1000;
    }

    protected virtual void Awake()
    {
        if (_renderer == null) {
            _renderer = GetComponent<Renderer>();
        }
    }

    protected virtual void Start()
    {
        SortingOrderFix();
    }

    protected void SortingOrderFix()
    {
        _renderer.sortingOrder = (int) (baseOrder - transform.position.y * orderFix + orderOffset);
    }
}
