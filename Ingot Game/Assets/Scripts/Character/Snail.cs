using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snail : MonoBehaviour
{
    [Header("Movement Config")]
    [SerializeField] private float accelerationSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedDecay;
    [SerializeField] private float minimumVelocity;
    [SerializeField] private float jumpVelocity;

    [Header("Ground Check Config")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Vector2 boxSize;

    [Header("Misc")]
    [SerializeField] private float snailHyperness = 0f;
    [SerializeField] private float snailTrust = 0f;

    // components
    private Rigidbody2D rb;
    private Animator animator;

    // internal variables
    private float directionX;
    private bool grounded;
    private bool jumpRequest;
    private bool isFacingRight;
    private float fallingTimer;
    private bool hidden;
    private bool waiting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        isFacingRight = true;

        if(snailHyperness == 0f)
        {
            snailHyperness = Random.Range(1f, 2.5f);
            Debug.Log("Snail Hyperness: " + snailHyperness);
        }

        if(snailTrust == 0f)
        {
            snailTrust = Random.Range(0.75f, 2.5f);
            Debug.Log("Snail Trust: " + snailTrust);
        }

        StartCoroutine(ChooseAction());
    }

    private void Update()
    {
        // if we aren't doing anything, do something
        if(!waiting && Mathf.Abs(rb.velocity.x) < 0.01f && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            waiting = true;

            StartCoroutine(ChooseAction());
        }

        // animate
        if(Mathf.Abs(rb.velocity.x) > 0.01f) animator.SetFloat("VelocityX", 0f);
        else animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

        animator.SetBool("Grounded", grounded);
    }

    private void FixedUpdate()
    {
        ApplyLinearSmooth();
        Move();

        if(jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        else
        {
            grounded = (Physics2D.OverlapBox(groundCheckPosition.position, boxSize, 0f, whatIsGround) != null);
        }
    }

    private void Move()
    {
        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        rb.AddForce(new Vector2(directionX, 0f) * accelerationSpeed);

        if(directionX > 0 && !isFacingRight) FlipSprite();
        else if(directionX < 0 && isFacingRight) FlipSprite();
    }

    private void ApplyLinearSmooth()
    {
        if(grounded && Mathf.Abs(directionX) < 0.01f) rb.velocity = new Vector2(rb.velocity.x * speedDecay, rb.velocity.y);
        if(Mathf.Abs(directionX) < 0.01f && Mathf.Abs(rb.velocity.x) < minimumVelocity && Mathf.Abs(rb.velocity.x) > 0.01f) rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    private void FlipSprite()
    {
        isFacingRight = !isFacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    #region Snail Actions

    IEnumerator ChooseAction()
    {
        Debug.Log("ChooseAction called");

        if(hidden) 
        {
            yield return new WaitForSeconds(Random.Range(snailHyperness, 30f) / snailTrust);

            ShellToggle();
            
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(snailHyperness, 18f) / snailHyperness);
        }

        Debug.Log("Now performing action");

        int action = Random.Range(0, 3);

        switch (action)
        {
            case 0:
                StartCoroutine(Progress());
                break;
            case 1:
                ShellToggle();
                break;
            case 2:
                FlipLeft();
                break;
            case 3:
                FlipRight();
                break;
        }   
    }

    IEnumerator Progress()
    {
        directionX = 0f;

        if (Random.Range(0, 2) == 0) directionX = 1f;
        else directionX = -1f;

        yield return new WaitForSeconds(Random.Range(0.25f, 2.0f) * snailHyperness);

        directionX = 0f;

        waiting = false;
    }

    public void ShellToggle()
    {
        if (!hidden)
        {
            animator.SetBool("Hidden", true);
            animator.SetBool("Hiding", true);

            hidden = true;
        }
        else
        {
            animator.SetBool("Escaping", true);
            hidden = false;
        }  
        
        waiting = false;   
    }

    public void FlipRight()
    {
        rb.velocity = Vector2.zero;

        if(Mathf.Abs(rb.rotation % 360) <= 5f || Mathf.Abs(rb.rotation % 360) >= 355) jumpRequest = true;
        rb.AddTorque(-0.75f, ForceMode2D.Impulse);

        waiting = false;
    }

    public void FlipLeft()
    {
        rb.velocity = Vector2.zero;

        if(Mathf.Abs(rb.rotation % 360) <= 5f || Mathf.Abs(rb.rotation % 360) >= 355) jumpRequest = true;
        rb.AddTorque(0.75f, ForceMode2D.Impulse);

        waiting = false;
    }

    #endregion

    public void Escaped()
    {
        animator.SetBool("Escaping", false);
        animator.SetBool("Hidden", false);
    }

    public void Hid()
    {
        animator.SetBool("Hiding", false);
    }
}
