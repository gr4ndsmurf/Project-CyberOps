using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionSystem))]
public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject lockpicking;


    public void Interact()
    {
        lockpicking.SetActive(true);
        AudioManager.Instance.Play("InteractChest");
        Debug.Log("Chest");
    }
}
