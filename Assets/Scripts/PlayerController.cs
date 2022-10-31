using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    private Rigidbody2D rb;
    private Transform groundCheck;
    private LayerMask groundLayer;

    private float coyeteTime = 0.2f;
    private float coyeteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    Vector3 MovementVector { get; }
    private float horizontal;
    public float Speed { get; set; }
    public float JumpingPower { get; set; }

    private bool isFacingRight = true;

    
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        groundLayer = LayerMask.GetMask("Ground");
    }

    void IController.Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyeteTimeCounter = coyeteTime;
        }
        else
        {
            coyeteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && coyeteTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpingPower);

            jumpBufferCounter = 0;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyeteTimeCounter = 0;
        }

        Flip();
        rb.velocity = new Vector2(horizontal * Speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
