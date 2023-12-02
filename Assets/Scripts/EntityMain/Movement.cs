using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [Tooltip("Default: 0.75")]
    protected float speed;
    [Tooltip("Default: 0.75")]
    [SerializeField] protected float walkingSpeed;
    [Tooltip("Default: 2")]
    [SerializeField] protected float runningSpeed;
    [Tooltip("Default: 3.5")]
    [SerializeField] protected float jumpForce;
    protected CapsuleCollider2D capsuleCollider2D;
    protected Rigidbody2D rb;

    [SerializeField] protected LayerMask platformLayerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }
    public abstract void Move();

    protected bool IsGrounded()
    {
        float extraHeightText = 0.02f;
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size - new Vector3(0.1f, 0f, 0f), CapsuleDirection2D.Vertical, 0f, Vector2.down, extraHeightText, platformLayerMask);
        
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(capsuleCollider2D.bounds.center + new Vector3(capsuleCollider2D.bounds.extents.x, 0), Vector2.down * (capsuleCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x, 0), Vector2.down * (capsuleCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x, capsuleCollider2D.bounds.extents.y), Vector2.right * (capsuleCollider2D.bounds.extents.x), rayColor);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

}
