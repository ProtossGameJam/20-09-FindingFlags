using System.Collections;
using Photon.Pun;
using UnityEngine;

public class PlayerAnimation : MonoBehaviourPun {
    [SerializeField] private Animator animator;

    [SerializeField] private string velocityParameterName;

    private PlayerInput moveInput;

    private void Awake() {
        if (animator == null) animator = GetComponent<Animator>();

        moveInput = GetComponentInParent<PlayerInput>();
    }

    private void Update() {
        if (photonView.IsMine) WalkAnimation();
    }

    private void WalkAnimation() {
        animator.SetFloat(velocityParameterName, moveInput.MoveVector.magnitude);
    }

    public void FlagGetAnimation() {
        StartCoroutine(PlayJumpAnimation());
    }

    private IEnumerator PlayJumpAnimation() {
        moveInput.isAllowMoveMine = false;
        animator.Play(Animator.StringToHash("Jump"));
        yield return new WaitForSeconds(3.3f);
        moveInput.isAllowMoveMine = true;
    }
}