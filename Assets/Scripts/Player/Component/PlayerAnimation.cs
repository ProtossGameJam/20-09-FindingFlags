using System;
using Photon.Pun;
using UnityEngine;

public class PlayerAnimation : MonoBehaviourPun
{
    [SerializeField] private Animator animator;

    private IMoveInput moveInput;

    [SerializeField] private string velocityParameterName;

    private void Awake()
    {
        if (animator == null) {
            animator = GetComponent<Animator>();
        }
        
        moveInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        WalkAnimation();
    }

    private void WalkAnimation()
    {
        if (photonView.IsMine) {
            animator.SetFloat(velocityParameterName, moveInput.MoveVector.magnitude);
        }
    }
}