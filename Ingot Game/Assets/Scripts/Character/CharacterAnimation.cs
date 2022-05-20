using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator baseAnimator;
    [SerializeField] private Animator[] dripAnimators;
    private CharacterMovement characterMovement;
    private Rigidbody2D rb; 

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool grounded = characterMovement.grounded;
        bool crouching = characterMovement.crouching;

        if(Mathf.Abs(rb.velocity.x) < 0.01f) baseAnimator.SetFloat("VelocityX", 0f);
        else baseAnimator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
        if(Mathf.Abs(rb.velocity.y) < 0.01f) baseAnimator.SetFloat("VelocityY", 0f);
        else baseAnimator.SetFloat("VelocityY", rb.velocity.y);

        baseAnimator.SetBool("Grounded", grounded);
        baseAnimator.SetBool("Crouching", crouching);

        if(Mathf.Abs(rb.velocity.x) < 0.01f)
        {
            foreach(Animator animator in dripAnimators)
            {
                animator.SetFloat("VelocityX", 0f);
            }
        }
        else
        {
            foreach(Animator animator in dripAnimators)
            {
                animator.SetFloat("VelocityX", Mathf.Abs(rb.velocity.x));
            }
        }
        if(Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            foreach(Animator animator in dripAnimators)
            {
                animator.SetFloat("VelocityY", 0f);
            }
        }
        else
        {
            foreach(Animator animator in dripAnimators)
            {
                animator.SetFloat("VelocityY", rb.velocity.y);
            }
        }

        foreach(Animator animator in dripAnimators)
        {
            animator.SetBool("Grounded", grounded);
            animator.SetBool("Crouching", crouching);
        }
    }
}
