using UnityEngine;

public class InteractModule : MonoBehaviour, IInteractable {
    public bool IsInteractable { get; set; }

    public virtual void Interact() { }
}