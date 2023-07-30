using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int health;
    [SerializeField] protected float speed;
    [SerializeField] protected float walkingSpeed;
    [SerializeField] protected float runningSpeed;
    [SerializeField] protected float jumpForce;
    protected Rigidbody2D rb;
    protected Animator anim;
    public bool isDestroyed;

    public abstract void Move();

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DestroyCharacter();
        }
    }
    public void Flip()
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

    protected virtual void DestroyCharacter()
    {
        isDestroyed = true;
        Destroy(gameObject);
    }
}
