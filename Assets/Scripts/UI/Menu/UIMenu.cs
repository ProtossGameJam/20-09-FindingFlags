using UnityEngine;
using UnityEngine.Events;

public class UIMenu : MonoBehaviour
{
    public GameObject rootMenuObject;

    [SerializeField] private UnityEvent enableEvent;

    private void OnEnable() { enableEvent.Invoke(); }
}