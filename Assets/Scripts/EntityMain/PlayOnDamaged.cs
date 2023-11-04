using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayOnDamaged : MonoBehaviour
{
    Health _health = null;
    [SerializeField] private Animator animator;

    [SerializeField] private string damagedAnimName;

    [SerializeField] private string killedAnimName;

    bool killed = false;
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
        if (_health.CurrentHealth > 0)
        {
            AudioManager.Instance.Play("DamagedSound");
            animator.Play(damagedAnimName);
            animator.keepAnimatorStateOnDisable = true;
        }
    }

    void OnKilled()
    {
        if (!killed)
        {
            AudioManager.Instance.Play("KilledSound");
            animator.Play(killedAnimName);
            //animator.keepAnimatorStateOnDisable = true;
            killed = true;
        }
    }
}
