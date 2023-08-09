using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] int _damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health?.TakeDamage(_damage);
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.SetActive(false);
        }
    }
}
