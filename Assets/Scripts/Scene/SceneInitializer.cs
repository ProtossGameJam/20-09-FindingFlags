using UnityEngine;
using UnityEngine.Events;

public class SceneInitializer : MonoBehaviour {
    [SerializeField] private UnityEvent startEvent;

    private void Start() {
        startEvent.Invoke();
    }
}