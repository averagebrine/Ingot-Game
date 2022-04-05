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
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private float sprintAccelerationMultiplier;

    [Header("Jump Config")]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float defaultGravity;

    [Header("Ground Check Config")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Vector2 boxSize;

    [Header("Player Collider Config")]
    [SerializeField] private Vector2 colliderSize;
    [SerializeField] private Vector2 crouchSizeMultiplier;
    [SerializeField] private float regOffset;
    [SerializeField] private float crouchOffset;

    [Header("Misc")]
    [SerializeField] private Transform ppCam;
    

    private Rigidbody2D rb;
    private float directionX;
    private bool sprint;
    private float _drag;
    private bool isFacingRight = true;

    private bool grounded;
    private bool jumpRequest;

    // "Wake the fuck up" -bungot brine
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && grounded)
        {
            jumpRequest = true;
        }

        if(Input.GetButton("Sprint")) sprint = true;
        else sprint = false;

        // Until Cinemachine
        ppCam.position = new Vector3(transform.position.x, transform.position.y, ppCam.position.z);
    }

    private void FixedUpdate()
    {
        ApplyLinearSmooth();
        Move();

        // Jump
        if(jumpRequest)
        {
            // isJumping = true;
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        else    // Check if grounded
        {
            grounded = (Physics2D.OverlapBox(groundCheckPosition.position, boxSize, 0f, whatIsGround) != null);
        }

        // Higher gravity when falling 
        // Velocity set to 0 if jump is cancelled
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = defaultGravity * fallMultiplier;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = defaultGravity * lowJumpMultiplier;
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
        if(sprint)
        {   // When sprinting
            if(Mathf.Abs(rb.velocity.x) > maxSpeed * sprintMultiplier)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * (maxSpeed * sprintMultiplier), rb.velocity.y);
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // Move character
        if(sprint) rb.AddForce(new Vector2(directionX, 0f) * (accelerationSpeed * sprintAccelerationMultiplier));
        else rb.AddForce(new Vector2(directionX, 0f) * accelerationSpeed);


        // Not sure if this should be called in Update or FixedUpdate ¯\_(ツ)_/¯
        if(directionX > 0 && !isFacingRight) Flip();
        else if(directionX < 0 && isFacingRight) Flip();
    }

    // Slow the character down if on the ground and not trying to move
    private void ApplyLinearSmooth()
    {
        if(grounded && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f) rb.velocity = new Vector2(rb.velocity.x * speedDecay, rb.velocity.y);
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f && Mathf.Abs(rb.velocity.x) < minimumVelocity && Mathf.Abs(rb.velocity.x) > 0.01f) rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }
}
