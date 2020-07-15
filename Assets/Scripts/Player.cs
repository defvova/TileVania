using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool facingRight;
    private float horizontalValue;

    [SerializeField] private float acceleration = 712f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Run();
        FlipSprite();
    }

    private void FlipSprite()
    {
        if ((horizontalValue < 0 && !facingRight) || (horizontalValue > 0 && facingRight))
        {
            facingRight = !facingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    private void Run()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
        Vector2 force = new Vector2(horizontalValue, rb.velocity.y);

        rb.AddForce(force * acceleration * Time.deltaTime);
    }
}