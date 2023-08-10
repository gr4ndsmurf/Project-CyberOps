using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionSystem))]
public class Chest : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Chest");
    }
}
