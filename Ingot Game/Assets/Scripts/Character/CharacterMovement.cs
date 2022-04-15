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
    [SerializeField] private float crouchMultiplier;
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

    [Header("Ceiling Check Config")]
    [SerializeField] private Collider2D crouchDisableCollider; 
    [SerializeField] private Transform ceilingCheckPosition;
    [SerializeField] private Vector2 ceilingCheckSize;

    [Header("Misc")]

    private Rigidbody2D rb;
    private float directionX;
    private bool sprint;
    private bool isFacingRight = true;

    private bool grounded;
    private bool jumpRequest;
    private bool crouchRequest;
    private bool crouching;

    private Animator animator;
    private float characterX;
    private float characterY;
    private bool characterGrounded;
    private bool characterJumping;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");

        if(!crouching && grounded && Input.GetButtonDown("Jump")) jumpRequest = true;

        if(grounded && Input.GetButton("Crouch")) crouchRequest = true;
        else crouchRequest = false;

        if(!crouching && Input.GetButton("Sprint")) sprint = true;
        else sprint = false;

        // unity tends to make the velocity a very small number instead of zero 
        if(Mathf.Abs(rb.velocity.x) < 0.01f) animator.SetFloat("VelocityX", 0f);
        else animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

        if(Mathf.Abs(rb.velocity.y) < 0.01f) animator.SetFloat("VelocityY", 0f);
        else animator.SetFloat("VelocityY", rb.velocity.y);

        // should this be done in FixedUpdate where the grounded bool is set?
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Crouching", crouching);
    }

    private void FixedUpdate()
    {
        ApplyLinearSmooth();
        Move();

        if(!crouching && jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        else
        {
            grounded = (Physics2D.OverlapBox(groundCheckPosition.position, boxSize, 0f, whatIsGround) != null);
        }

        // higher gravity when falling 
        // velocity set to 0 if jump is cancelled --change that maybe
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = defaultGravity * fallMultiplier;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = defaultGravity * lowJumpMultiplier;
            // rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }


        if(grounded && crouchRequest) crouching = true;
        else if(grounded && !crouchRequest && Physics2D.OverlapBox(ceilingCheckPosition.position, ceilingCheckSize, 0f, whatIsGround)) crouching = true;
        else crouching = false;

        if(crouching) crouchDisableCollider.enabled = false;
        else crouchDisableCollider.enabled = true;
    }

    private void Move()
    {
        // clamp speed
        if(sprint)
        {   // when sprinting
            if(Mathf.Abs(rb.velocity.x) > maxSpeed * sprintMultiplier)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * (maxSpeed * sprintMultiplier), rb.velocity.y);
            }
        }
        else if(crouching)
        {   // when crouching
            if(Mathf.Abs(rb.velocity.x) > maxSpeed * crouchMultiplier)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * (maxSpeed * crouchMultiplier), rb.velocity.y);
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        // move character
        if(sprint) rb.AddForce(new Vector2(directionX, 0f) * (accelerationSpeed * sprintAccelerationMultiplier));
        else rb.AddForce(new Vector2(directionX, 0f) * accelerationSpeed);


        // not sure if this should be called in Update or FixedUpdate ¯\_(ツ)_/¯
        if(directionX > 0 && !isFacingRight) Flip();
        else if(directionX < 0 && isFacingRight) Flip();
    }

    // slow the character down if on the ground and not trying to move
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
