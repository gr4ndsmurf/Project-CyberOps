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

    [SerializeField] private bool player;
    [SerializeField] private GameObject[] weaponHolderObjects;
    [SerializeField] WeaponSwitching wp;
    [SerializeField] private Animator[] weaponsAnimator;
    [SerializeField] private Animator[] upperBody;
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
            //if PlayOnDamaged.cs is player's script.
            if (player)
            {
                animator = weaponsAnimator[wp.selectedWeapon];
                upperBody[wp.selectedWeapon].Play(damagedAnimName);
            }

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

            //if PlayOnDamaged.cs is player's script.
            if (player)
            {
                weaponHolderObjects[0].SetActive(true);
                weaponHolderObjects[1].SetActive(false);
                weaponHolderObjects[2].SetActive(false);
                animator = weaponsAnimator[0];
            }
            animator.Play(killedAnimName);
            //animator.keepAnimatorStateOnDisable = true;
            killed = true;
        }
    }
}
