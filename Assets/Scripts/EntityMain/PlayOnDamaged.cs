using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayOnDamaged : MonoBehaviour
{
    Health _health = null;
    [SerializeField] Color damageColor;
    [SerializeField] SpriteRenderer targetSR;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Damaged += OnDamaged;
        _health.Killed += OnKilled;
    }

    private void OnDisable()
    {
        _health.Damaged -= OnDamaged;
        _health.Killed -= OnKilled;
    }

    void OnDamaged(int damage)
    {
        AudioManager.Instance.Play("DamagedSound");
    }

    void OnKilled()
    {
        AudioManager.Instance.Play("KilledSound");
    }
}
