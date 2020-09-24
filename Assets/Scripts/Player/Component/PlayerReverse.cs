using UnityEngine;

public class PlayerReverse : MonoBehaviour {
    [SerializeField] private Transform reverseTransform;

    [SerializeField] private bool isXFlip;

    private IMoveInput moveInput;

    private void Awake() {
        if (reverseTransform == null) reverseTransform = GetComponent<Transform>();

        moveInput = FindObjectOfType<PlayerInput>();
    }

    private void Update() {
        FlipPlayer(moveInput.MoveVector.x);
    }

    public void FlipPlayer(float vecXValue) {
        if (vecXValue > 0.0f) {
            if (!isXFlip) {
                isXFlip = true;

                var localScale = reverseTransform.localScale;
                localScale = new Vector3(-1.0f * localScale.x, localScale.y);
                reverseTransform.localScale = localScale;
            }
        }
        else if (vecXValue < 0.0f) {
            if (isXFlip) {
                isXFlip = false;

                var localScale = reverseTransform.localScale;
                localScale = new Vector3(-1.0f * localScale.x, localScale.y);
                reverseTransform.localScale = localScale;
            }
        }
    }
}