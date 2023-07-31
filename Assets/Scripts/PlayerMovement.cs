using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Movement
{
    public bool canJump;

    private void Update()
    {
        if (IsGrounded())
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
        Jump();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        Running();
    }
    public void Running()
    {
        speed = walkingSpeed;
        if (rb.velocity.x < 0 || rb.velocity.x > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runningSpeed;
                if (canJump)
                {
                }
            }
            
        }
    }

    public void Jump()
    {
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    
}
