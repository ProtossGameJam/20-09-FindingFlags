using UnityEngine;
using UnityEngine.Events;

public class InitializeSceneManager : MonoBehaviour
{
    [SerializeField] private UnityEvent startEvent;

    private void Start() {
        startEvent.Invoke();
    }
}