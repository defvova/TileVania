using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float acceleration = 600f;
    [SerializeField] private float jumpForce = 1200f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    private Vector3 currentVelocity = Vector3.zero;
    private bool isJump = false;

    private Animator animator;
    private Rigidbody2D rb;
    private bool facingRight;
    private float horizontalValue;
    private Collider2D collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")) isJump = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        MoveSprite();
        FlipSprite();
        Jump();
    }

    private void Jump()
    {
        if (isJump && IsFloor())
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJump = false;
        }
    }

    private bool IsFloor()
    {
        return collider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    private void FlipSprite()
    {
        if ((horizontalValue < 0 && !facingRight) || (horizontalValue > 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    private void MovePlayer()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        Vector3 targetVelocity = new Vector2(GetSpeed(), rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
    }

    private void MoveSprite()
    {
        animator.SetBool("isRunning", IsRunning());
    }

    private float GetSpeed()
    {
        return horizontalValue * acceleration * Time.fixedDeltaTime;
    }

    private bool IsRunning()
    {
        return Mathf.Abs(GetSpeed()) > Mathf.Epsilon;
    }
}