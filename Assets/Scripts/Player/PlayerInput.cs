using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IMoveInput
{
    public Vector2 MoveVector { get; set; }

    private void Awake()
    {
        InputHandler.MovePerformActionRegister(MoveInput);
        InputHandler.MoveCanceledActionRegister((ctx) => {
            MoveVector = Vector2.zero;
        });
    }

    private void MoveInput(InputAction.CallbackContext ctx)
    {
        MoveVector = ctx.ReadValue<Vector2>().normalized;
    }
}