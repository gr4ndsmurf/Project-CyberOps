using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int amount = 50;

    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GameManager.Instance.HealthPotion > 0 && health.CurrentHealth < health.MaxHealth && GameManager.Instance.canAttack)
            {
                health.Heal(amount);
                GameManager.Instance.HealthPotion -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            health.TakeDamage(amount);
        }
    }
}
