using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] int _damage = 20;

    [SerializeField] bool playerbullet = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerbullet)
        {
            if (collision.CompareTag("Target"))
            {
                Health health = collision.gameObject.GetComponent<Health>();
                health?.TakeDamage(_damage);
                //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                Health health = collision.gameObject.GetComponent<Health>();
                health?.TakeDamage(_damage);
                //gameObject.GetComponent<BoxCollider2D>().enabled = false;
                gameObject.SetActive(false);
            }
        }

        if (collision.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
        }

    }
}
