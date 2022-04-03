using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Config")]
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedDecay;
    [SerializeField] private float minimumVelocity;

    [Header("Jump Config")]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private float defaultGravity;

    [Header("Ground Check Config")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Vector2 boxSize;
    

    private Rigidbody2D rb;
    private float directionX;
    private float _drag;

    private bool grounded;
    private bool jumpRequest;

    // "Wake the fuck up" -bungot brine
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // This is way better than that new input system package :)
        directionX = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && grounded)
        {
            jumpRequest = true;
        }
    }

    private void FixedUpdate()
    {
        ApplyLinearSmooth();
        Move();

        // Jump
        if(jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        else
        {   // Check if grounded
            grounded = (Physics2D.OverlapBox(groundCheckPosition.position, boxSize, 0f, whatIsGround) != null);
        }

        // Higher gravity when falling 
        // Velocity set to 0 if jump is cancelled
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }

    }

    private void Move()
    {
        // Clamp speed
        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // Move character
        rb.AddForce(new Vector2(directionX, 0f) * accelerationSpeed);
    }

    // Slow the character down if on the ground and not trying to move
    private void ApplyLinearSmooth()
    {
        if(grounded && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.1f) rb.velocity = new Vector2(rb.velocity.x * speedDecay, rb.velocity.y);
        if(Mathf.Abs(rb.velocity.x) < minimumVelocity) rb.velocity = new Vector2(0f, rb.velocity.y);
    }
}