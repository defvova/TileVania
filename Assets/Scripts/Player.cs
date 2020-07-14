using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float acceleration = 712f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 force = new Vector2(x, rb.velocity.y);

        rb.AddForce(force * acceleration * Time.deltaTime);
    }
}