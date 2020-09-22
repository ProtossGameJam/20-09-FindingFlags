using UnityEngine;
using UnityEngine.Events;

public class UnityEventCallbacks : MonoBehaviour
{
    [SerializeField] private UnityEvent awakeCallback;
    [SerializeField] private UnityEvent enableCallback;
    [SerializeField] private UnityEvent startCallback;
    [SerializeField] private UnityEvent updateCallback;
    [SerializeField] private UnityEvent lateUpdateCallback;
    [SerializeField] private UnityEvent applicationQuitCallback;
    [SerializeField] private UnityEvent disableCallback;
    [SerializeField] private UnityEvent destroyCallback;

    private void Awake() { awakeCallback.Invoke(); }

    private void Start() { startCallback.Invoke(); }

    private void Update() { updateCallback.Invoke(); }

    private void LateUpdate() { lateUpdateCallback.Invoke(); }

    private void OnEnable() { enableCallback.Invoke(); }

    private void OnDisable() { disableCallback.Invoke(); }

    private void OnDestroy() { destroyCallback.Invoke(); }

    private void OnApplicationQuit() { applicationQuitCallback.Invoke(); }
}