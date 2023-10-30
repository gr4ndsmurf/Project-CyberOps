using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionSystem))]
public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject lockpicking;

    public void Interact()
    {
        Cursor.visible = true;
        lockpicking.SetActive(true);
        GameManager.Instance.canAttack = false;
        GameManager.Instance.canMove = false;
        AudioManager.Instance.Play("InteractChest");
        Debug.Log("Chest");
    }
}
