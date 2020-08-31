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

    private void Update()
    {
        Move(rigidbody, moveInput.MoveVector, moveSpeed * Time.deltaTime);
    }

    private void Move(Rigidbody2D rigidbody, Vector2 vec, float speed)
    {
        // TODO: Player collider dug in to another collider
        rigidbody.position += vec * speed;
    }
    private void Move(Transform transform, Vector3 vec, float speed)
    {
        transform.position += vec * speed;
    }
}