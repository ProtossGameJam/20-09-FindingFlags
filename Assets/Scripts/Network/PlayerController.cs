using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IMoveInput
{
    //[SerializeField] GameObject cameraHolder;
    [SerializeField] float walkSpeed;

    public Vector2 MoveVector { get; set; }
    Vector2 moveAmount;

    Rigidbody2D rb;

    PhotonView pv;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();

        InputHandler.MovePerformActionRegister(MoveInput);
        InputHandler.MoveCanceledActionRegister((ctx) => {
            MoveVector = Vector2.zero;
        });
    }

    private void Start()
    {
        if(!pv.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }

    void Update()
    {
        if(!pv.IsMine)
        {
            return;
        }
        Move();
    }

    void Move()
    {
        //Input 시스템이 Input System Package로 바뀐듯 하다. 그걸로 다시 바꿔줘야함.
        //Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        //Vector2 moveDir = new Vector2(InputSystem.GetDevice<Keyboard>()., Input.GetAxisRaw("Vertical")).normalized;
        //moveAmount = moveDir * walkSpeed;
    }
    private void MoveInput(InputAction.CallbackContext ctx)
    {
        MoveVector = ctx.ReadValue<Vector2>().normalized;
        moveAmount = MoveVector * walkSpeed;
    }

    private void FixedUpdate()
    {
        if (!pv.IsMine)
        {
            return;
        }
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }
}
