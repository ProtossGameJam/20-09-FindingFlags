using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    public void SetMovementEntire(bool enable) {
        PlayerInput.IsAllowMovement = enable;
    }
}