using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int health;
    [Tooltip("Default: 0.75")]
    protected float speed;
    [Tooltip("Default: 0.75")]
    [SerializeField] protected float walkingSpeed;
    [Tooltip("Default: 2")]
    [SerializeField] protected float runningSpeed;
    [Tooltip("Default: 3.5")]
    [SerializeField] protected float jumpForce;
    protected BoxCollider2D boxCollider2d;
    protected Rigidbody2D rb;
    public bool isDestroyed;

    [SerializeField] protected LayerMask platformLayerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }
    public abstract void Move();

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyCharacter();
        }
    }
    protected bool IsGrounded()
    {
        float extraHeightText = 0.02f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down, extraHeightText, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y), Vector2.right * (boxCollider2d.bounds.extents.x), rayColor);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
    protected virtual void DestroyCharacter()
    {
        isDestroyed = true;
        Destroy(gameObject);
    }

}
