using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InitializeEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent awakeCallback;
    [SerializeField] private UnityEvent startCallback;
    
    private void Awake()
    {
        awakeCallback.Invoke();
    }

    private void Start()
    {
        startCallback.Invoke();
    }
}
