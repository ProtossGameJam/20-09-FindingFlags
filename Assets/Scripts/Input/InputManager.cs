using System;
using UnityEngine;

public class InputManager {
    public static void InputAction(KeyCode key, Action action) {
        if (Input.GetKeyDown(key)) action.Invoke();
    }
}