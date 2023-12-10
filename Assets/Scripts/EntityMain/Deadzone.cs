using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    [SerializeField] int _damage = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health?.TakeDamage(_damage);
        }

        if (collision.CompareTag("Target"))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health?.TakeDamage(_damage);
        }
    }
}
