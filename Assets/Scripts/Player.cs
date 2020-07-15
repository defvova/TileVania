using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float acceleration = 500f;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    private Rigidbody2D rb;
    private bool facingRight;
    private float horizontalValue;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        Flip();
    }

    private void Flip()
    {
        if ((horizontalValue < 0 && !facingRight) || (horizontalValue > 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    private void Move()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        float xSpeed = horizontalValue * acceleration * Time.fixedDeltaTime;
        Vector3 targetVelocity = new Vector2(xSpeed, rb.velocity.y);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
    }
}