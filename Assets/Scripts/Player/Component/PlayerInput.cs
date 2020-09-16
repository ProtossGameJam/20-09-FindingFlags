using Photon.Pun;
using UnityEngine;

public class PlayerInput : MonoBehaviourPun, IMoveInput
{
    private Vector2 _moveVec;
    
    public Vector2 MoveVector
    {
        get => _moveVec.normalized;
        set => _moveVec = value;
    }

    private void Update()
    {
        if (photonView.IsMine) {
            _moveVec.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}