using Photon.Pun;
using UnityEngine;

public class PlayerInput : MonoBehaviourPun, IMoveInput {
    private Vector2 _moveVec;

    [ReadOnly] public static bool IsAllowMovement;

    private void Update() {
        if (IsAllowMovement) {
            if (photonView.IsMine) {
                _moveVec.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
        }
        
    }

    public Vector2 MoveVector { get => _moveVec.normalized; set => _moveVec = value; }
}