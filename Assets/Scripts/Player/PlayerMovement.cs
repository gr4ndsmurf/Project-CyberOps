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

        if (GetComponentInChildren<Health>().CurrentHealth <= 0)
        {
            GameManager.Instance.isDead = true;
        }
        else
        {
            GameManager.Instance.isDead = false;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        if (GameManager.Instance.canMove)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            Running();
        }
    }
    public void Running()
    {
        speed = walkingSpeed;
        if (rb.velocity.x < 0 || rb.velocity.x > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runningSpeed;
            }
            
        }
    }

    public void Jump()
    {
        if (canJump && GameManager.Instance.canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    
}
