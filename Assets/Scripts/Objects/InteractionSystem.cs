using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private bool canInteract;
    [SerializeField] private GameObject interactCanvas;

    public Transform playerTransform; // Oyuncu Transform bileþeni
    public float triggerDistance = 1.0f; // Belirli seviyeye gelme mesafesi

    private void Update()
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
        }
    }
    private void OnMouseExit()
    {
        if (canInteract)
        {
            interactCanvas.SetActive(false);
        }
    }

    private void Interact()
    {
        Debug.Log("Interaction with the object");
    }

}
