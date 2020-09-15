using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IMoveInput
{
    [SerializeField] private PhotonView pView;
    
    public Vector2 MoveVector { get; set; }

    private void Awake()
    {
        if (pView == null) {
            pView = GetComponent<PhotonView>();
        }
        
        InputHandler.MovePerformActionRegister(MoveInput);
        InputHandler.MoveCanceledActionRegister((ctx) => {
            MoveVector = Vector2.zero;
        });
    }

    private void MoveInput(InputAction.CallbackContext ctx)
    {
        if (pView.IsMine) {
            MoveVector = ctx.ReadValue<Vector2>().normalized;
        }
    }
}