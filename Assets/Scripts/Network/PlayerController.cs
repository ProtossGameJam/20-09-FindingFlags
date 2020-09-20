using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] GameObject cameraHolder;
    [SerializeField] float walkSpeed;

    Vector2 moveAmount;

    Rigidbody2D rb;

    PhotonView pv;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
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
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = moveDir * walkSpeed;
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
