using Photon.Pun;
using UnityEngine;

public class PlayerInput : MonoBehaviourPun, IMoveInput {
    [ReadOnly] public static bool IsAllowMovement;
    public bool isAllowMoveMine = true;
    private Vector2 _moveVec;

    private void Update() {
        if (IsAllowMovement) {
            if (photonView.IsMine) {
                _moveVec.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
        }
    }

    public Vector2 MoveVector {
        get => isAllowMoveMine ? _moveVec.normalized : Vector2.zero;
        set => _moveVec = value;
    }
}