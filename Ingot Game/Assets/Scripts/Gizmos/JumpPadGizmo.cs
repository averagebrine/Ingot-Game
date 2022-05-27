using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadGizmo : MonoBehaviour
{
    [SerializeField] private float jumpHeight = 6f;
    [SerializeField] private Transform raycastPosition;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Jump(col.GetComponent<Rigidbody2D>());
        }
    }

    private void Jump(Rigidbody2D rb)
    {
        if (rb.GetComponent<CharacterMovement>().jumpPad) return;
        if (!rb.GetComponent<CharacterMovement>().grounded) return;

        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    
        rb.GetComponent<CharacterMovement>().jumpPad = true;
    }
}
