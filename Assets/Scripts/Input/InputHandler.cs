using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private static PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public static void MovePerformActionRegister(Action<InputAction.CallbackContext> action)
    {
        playerControls.Player.Move.performed += action;
    }

    public static void MoveCanceledActionRegister(Action<InputAction.CallbackContext> action)
    {
        playerControls.Player.Move.canceled += action;
    }
}
