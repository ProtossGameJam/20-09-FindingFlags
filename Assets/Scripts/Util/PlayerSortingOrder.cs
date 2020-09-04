using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlayerSortingOrder : SpriteSortingOrder
{
    private void Update()
    {
        SortingOrderFix();
    }
}
