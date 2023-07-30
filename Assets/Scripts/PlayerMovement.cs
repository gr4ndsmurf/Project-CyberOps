using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    private void Update()
    {
        if (IsGrounded())
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        Move();
        Flip();
    }

    public override void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        Running();

        if (rb.velocity.x < 0 || rb.velocity.x > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
    public void Running()
    {
        speed = walkingSpeed;
        anim.SetBool("isRunning", false);
        if (rb.velocity.x < 0 || rb.velocity.x > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runningSpeed;
                anim.SetBool("isRunning", true);
            }
        }
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
    }

    
}
