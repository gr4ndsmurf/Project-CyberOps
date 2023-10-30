using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionSystem))]
public class Computer : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject MorseCodeCanvas;

    public void Interact()
    {
        Cursor.visible = true;
        MorseCodeCanvas.SetActive(true);
        GameManager.Instance.canAttack = false;
        GameManager.Instance.canMove = false;
        //AudioManager.Instance.Play("InteractComputer");
        Debug.Log("Computer");
    }
}
