using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private SpriteSortingOrder orderFixer;
    
    private void Update()
    {
        orderFixer.SortingOrderFix();
    }
}
