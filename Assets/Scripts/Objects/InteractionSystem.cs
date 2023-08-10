using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private bool canInteract;
    [SerializeField] private GameObject interactCanvas;

    public Transform playerTransform;
    public float triggerDistance = 1.0f;

    [SerializeField] private WeaponSwitching wS;
    [SerializeField] private Crosshair crosshair;

    private IInteractable currentInteractable;

    private void Start()
    {
        currentInteractable = GetComponent<IInteractable>();
    }
    private void Update()
    {
        if (wS.selectedWeapon == 0)
        {
            float distance = Vector2.Distance(playerTransform.position, transform.position);
            if (distance <= triggerDistance)
            {
                canInteract = true;
            }
            else
            {
                canInteract = false;
                interactCanvas.SetActive(false);
            }
        }
        else
        {
            canInteract = false;
            interactCanvas.SetActive(false);
            crosshair.mouseOnObject = false;
        }
    }
    private void OnMouseDown()
    {
        if (canInteract)
        {
            Interact();
        }
    }
    private void OnMouseEnter()
    {
        if (canInteract)
        {
            interactCanvas.SetActive(true);
            crosshair.mouseOnObject = true;
        }
    }
    private void OnMouseExit()
    {
        if (canInteract)
        {
            interactCanvas.SetActive(false);
            crosshair.mouseOnObject = false;
        }
    }

    private void Interact()
    {
        currentInteractable.Interact();
    }

}
