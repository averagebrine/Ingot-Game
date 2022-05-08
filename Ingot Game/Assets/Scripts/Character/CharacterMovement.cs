using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    // components
    private Rigidbody2D rb;
    private Animator animator;

    // internal variables
    private float directionX;
    private bool grounded;
    private bool sprint;
    private bool jumpRequest;
    private bool crouchRequest;
    private bool crouching;
    private bool isFacingRight = true;
    private float fallingTimer;
    
    private void Awake()
    {
        // initialize
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // get input
        directionX = Input.GetAxisRaw("Horizontal");

        if(!crouching && grounded && Input.GetButtonDown("Jump")) jumpRequest = true;

        if(grounded && Input.GetButton("Crouch")) crouchRequest = true;
        else crouchRequest = false;

        if(!crouching && Input.GetButton("Sprint")) sprint = true;
        else sprint = false;

        // temporary screen shake
        if(!grounded && rb.velocity.y < 0f) fallingTimer += Time.deltaTime;
        if(grounded && fallingTimer > 1f) FindObjectOfType<ShakeMachine>().Shake();
        if (grounded) fallingTimer = 0f;

        // wow, this is compact.
        if(Mathf.Abs(rb.velocity.x) < 0.01f) animator.SetFloat("VelocityX", 0f);
        else animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        if(Mathf.Abs(rb.velocity.y) < 0.01f) animator.SetFloat("VelocityY", 0f);
        else animator.SetFloat("VelocityY", rb.velocity.y);

        animator.SetBool("Grounded", grounded);
        animator.SetBool("Crouching", crouching);
    }

    private void FixedUpdate()
    {
        ApplyLinearSmooth();
        Move();

        // boing
        if(!crouching && jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }       
        else
        {   // this is where we make sure the character is grounded
            grounded = (Physics2D.OverlapBox(groundCheckPosition.position, boxSize, 0f, whatIsGround) != null);
        }


        // this isn't the best way to do this, but it works
        // if the lowJumpMultiplier is too high, the character might fall too quickly :/
        if(rb.velocity.y < 0) rb.gravityScale = defaultGravity * fallMultiplier;
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump")) rb.gravityScale = defaultGravity * lowJumpMultiplier;
        else rb.gravityScale = defaultGravity;


        // sneak
        // compact if statements are hard to read
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
        }   // when not sprinting or crouching
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

    private void ApplyLinearSmooth()
    {
        // this is self explanatory, right?
        if(grounded && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f) rb.velocity = new Vector2(rb.velocity.x * speedDecay, rb.velocity.y);
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f && Mathf.Abs(rb.velocity.x) < minimumVelocity && Mathf.Abs(rb.velocity.x) > 0.01f) rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    private void Flip()
    {
        // confusing math :D
        isFacingRight = !isFacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }
}
