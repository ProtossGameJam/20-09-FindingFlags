using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTest : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current[Key.F].wasPressedThisFrame) {
            var colObj = Physics2D.OverlapCircle(transform.position, 1.0f);
            colObj.GetComponent<DialogueManager>().Interact();
        }
    }
}
