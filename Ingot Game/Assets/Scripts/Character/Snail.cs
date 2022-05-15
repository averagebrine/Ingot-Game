using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snail : MonoBehaviour
{
    #region Variables

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
    private bool canAction;

    #endregion

    #region Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        isFacingRight = true;

        GeneratePersonality();

        canAction = true;
    }

    private void Update()
    {
        // animate
        if(Mathf.Abs(rb.velocity.x) > 0.01f) animator.SetFloat("VelocityX", 0f);
        else animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));

        animator.SetBool("Grounded", grounded);

        if (canAction)
        {
            StartCoroutine(Action());
            canAction = false;
        }
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(snailTrust >= 3f)
            {
                return;
            }

            if(Random.Range(0.2f, 3f ) > snailTrust)
            {
                if(!hidden) StartCoroutine(ShellHide());

            }
        }
    }

    #endregion

    #region Snail Actions

    private void GeneratePersonality()
    {
        if(snailHyperness == 0f)
        {
            snailHyperness = Random.Range(0.5f, 3f);
        }

        if(snailTrust == 0f)
        {
            snailTrust = Random.Range(0.2f, 2f);
        }
    }

    IEnumerator Action()
    {
        yield return new WaitForSeconds(Random.Range(5f, 20f) / snailHyperness);

        int action = Random.Range(0, 5);

        switch (action)
        {
            case 0:
                StartCoroutine(ShellHide());
                break;
            case 1:
                StartCoroutine(FlipLeft());
                break;
            case 2:
                StartCoroutine(FlipRight());
                break;
            case 3:
                StartCoroutine(Progress());
                break;
            case 4:
                StartCoroutine(Progress());
                break;
            case 5:
                StartCoroutine(Action());
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

        canAction = true;
    }

    IEnumerator ShellHide()
    {
        canAction = false;

        animator.SetBool("Hidden", true);
        animator.SetBool("Hiding", true);

        hidden = true;

        float wait = Random.Range(10f, 30f) / snailTrust;

        yield return new WaitForSeconds(wait);

        StartCoroutine(ShellEscape());
    }

    IEnumerator ShellEscape()
    {        
        animator.SetBool("Escaping", true);

        hidden = false;

        yield return new WaitForSeconds(Random.Range(5f, 10f) / snailHyperness);

        canAction = true;
    }

    IEnumerator FlipLeft()
    {
        rb.velocity = Vector2.zero;

        if(Mathf.Abs(rb.rotation % 360) <= 5f || Mathf.Abs(rb.rotation % 360) >= 355) jumpRequest = true;
        rb.AddTorque(Random.Range(0.80f, 0.70f), ForceMode2D.Impulse);

        yield return new WaitForSeconds(Random.Range(1f, 5.0f) / snailHyperness);

        canAction = true;
    }

    IEnumerator FlipRight()
    {
        rb.velocity = Vector2.zero;

        if(Mathf.Abs(rb.rotation % 360) <= 5f || Mathf.Abs(rb.rotation % 360) >= 355) jumpRequest = true;
        rb.AddTorque(Random.Range(-0.80f, -0.70f), ForceMode2D.Impulse);

        yield return new WaitForSeconds(Random.Range(1f, 5.0f) / snailHyperness);

        canAction = true;
    }

    #endregion

    #region Movement

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

    public void Escaped()
    {
        animator.SetBool("Escaping", false);
        animator.SetBool("Hidden", false);
    }

    public void Hid()
    {
        animator.SetBool("Hiding", false);
    }

    #endregion
}