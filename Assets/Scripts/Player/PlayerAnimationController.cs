using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // Add this script to "EmptyPlayer" or any "PlayerWith..." gameobject in WeaponHolder.
    [Tooltip("Select This Object's Animator")]
    public Animator anim;
    [Tooltip("Select Player Movement Script")]
    public PlayerMovement pm;
    [Tooltip("Select Player Rigidbody2D")]
    public Rigidbody2D rb;

    private void Update()
    {
        if (GameManager.Instance.canMove)
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.canMove)
        {
            Walk();
            Run();
            Flip();
        }
    }

    public void Walk()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(moveInput));
    }
    private void Run()
    {
        anim.SetBool("isRunning", false);
        if (rb.velocity.x < 0 || rb.velocity.x > 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("isRunning", true);
                if (pm.canJump)
                {
                    AudioManager.Instance.Play("RunningSound");
                }
            }
        }
    }
    private void Jump()
    {
        if (!pm.canJump)
        {
            anim.SetBool("isJumping", true);
        }
        else if (pm.canJump)
        {
            anim.SetBool("isJumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.Instance.Play("JumpingSound");
            }
        }
    }
    private void Flip()
    {
        Vector3 characterScale = transform.localScale;
        if (rb.velocity.x < 0)
        {
            characterScale.x = -1;
        }
        if (rb.velocity.x > 0)
        {
            characterScale.x = 1;
        }
        transform.localScale = characterScale;
    }
}
