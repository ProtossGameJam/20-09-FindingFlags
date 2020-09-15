using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SpriteSortingOrder : SortingOrderBase
{
    [SerializeField] protected Renderer sortingRenderer;

    private void Awake()
    {
        if (sortingRenderer == null) {
            sortingRenderer = GetComponent<Renderer>();
        }
    }

    private void Start()
    {
        SortingOrderFix(sortingRenderer);
    }
}
