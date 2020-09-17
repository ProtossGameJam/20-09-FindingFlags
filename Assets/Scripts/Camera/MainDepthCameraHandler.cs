using UnityEngine;

public class MainDepthCameraHandler : MonoBehaviour
{
    [ReadOnly] [SerializeField] private Vector3 screenSize;
    [ReadOnly] [SerializeField] private Vector3 originalPosition;

    [SerializeField] private float sensitivity;

    [SerializeField] private Vector2 limitMovement;

    private void Start() {
        originalPosition = transform.position;
        screenSize = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
    }

    private void Update() {
        var fixVec = (Input.mousePosition - screenSize) / (sensitivity * 100.0f);
        transform.position = originalPosition
                             + new Vector3(
                                 Mathf.Clamp(fixVec.x, -limitMovement.x, limitMovement.x),
                                 Mathf.Clamp(fixVec.y, -limitMovement.y, limitMovement.y)
                             );
    }
}