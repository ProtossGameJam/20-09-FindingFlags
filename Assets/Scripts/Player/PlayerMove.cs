using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;

    private IMoveInput moveInput;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveInput = FindObjectOfType<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move(transform, moveInput.MoveVector, moveSpeed * Time.deltaTime);
    }

    private void Move(Transform player, Vector2 vec, float speed)
    {
        rigidbody.MovePosition((Vector2)player.position + vec * speed);
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = Mathf.Clamp(speed, 0.0f, float.PositiveInfinity);
    }
}