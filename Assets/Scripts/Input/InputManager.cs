using UnityEngine;

public class InputManager
{
    public static void InputAction(KeyCode key, System.Action action) {
        if (Input.GetKeyDown(key)) {
            action.Invoke();
        }
    }
}