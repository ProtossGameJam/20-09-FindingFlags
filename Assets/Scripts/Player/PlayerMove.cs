using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;

    private IMoveInput moveInput;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        moveInput = FindObjectOfType<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move(transform, moveInput.MoveVector, moveSpeed * Time.deltaTime);
    }

    private void Move(Transform player, Vector2 vec, float speed)
    {
        // TODO: Player collider dug in to another collider
        rigidbody.MovePosition((Vector2)player.position + vec * speed);
    }
}