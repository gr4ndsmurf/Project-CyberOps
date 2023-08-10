using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionSystem))]
public class Door : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Door");
    }
}
