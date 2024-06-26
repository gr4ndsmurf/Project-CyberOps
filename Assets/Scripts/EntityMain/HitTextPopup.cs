using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class HitTextPopup : MonoBehaviour
{
    [SerializeField] Health _healthToObserve = null;
    [SerializeField] TextMeshProUGUI _textPopupUI = null;

    [SerializeField] string _killText = "KILL";
    [SerializeField] float _textPopupDuration = 1;

    Health _observedHealth = null;
    Coroutine _popupRoutine = null;

    [SerializeField] private Transform entity;
    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        StartObservingHealth(_healthToObserve);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, entity.position, 1f) + offset;
    }

    public void StartObservingHealth(Health newHealthToObserver)
    {
        _observedHealth = newHealthToObserver;
        // notify when target is damaged
        _observedHealth.Damaged += OnObservedHealthDamaged;
        _observedHealth.Killed += OnObservedHealthKilled;
    }

    public void StopObservingHealth()
    {
        // no longer watch target
        _observedHealth.Damaged -= OnObservedHealthDamaged;
        _observedHealth.Killed -= OnObservedHealthKilled;

        _observedHealth = null;
    }

    void OnObservedHealthDamaged(int damaged)
    {
        string hitText = damaged.ToString();

        if (_popupRoutine != null)
            StopCoroutine(_popupRoutine);
        _popupRoutine = StartCoroutine(TextPopup(hitText));
    }

    IEnumerator TextPopup(string textToShow)
    {
        _textPopupUI.text = textToShow;
        yield return new WaitForSeconds(_textPopupDuration);
        _textPopupUI.text = string.Empty;
    }

    void OnObservedHealthKilled()
    {
        if (_popupRoutine != null)
            StopCoroutine(_popupRoutine);
        _popupRoutine = StartCoroutine(TextPopup(_killText));
        StopObservingHealth();
    }
}
