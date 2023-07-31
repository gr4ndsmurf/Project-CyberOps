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
        //Flip();
    }

    public override void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        //anim.SetFloat("Speed", Mathf.Abs(moveInput));
        Running();
    }
    public void Running()
    {
        speed = walkingSpeed;
        //anim.SetBool("isRunning", false);
        if (rb.velocity.x < 0 || rb.velocity.x > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runningSpeed;
                //anim.SetBool("isRunning", true);
                if (canJump)
                {
                    //AudioManager.Instance.Play("RunningSound");
                }
            }
            
        }
    }

    public void Jump()
    {
        if (!canJump)
        {
            //anim.SetBool("isJumping", true);
        }
        else if (canJump)
        {
            //anim.SetBool("isJumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //AudioManager.Instance.Play("JumpingSound");
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    
}
