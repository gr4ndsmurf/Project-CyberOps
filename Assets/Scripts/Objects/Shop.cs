using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionSystem))]
public class Shop : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Cursor.visible = true;
        ShopManager.Instance.shopCanvas.SetActive(true);
        GameManager.Instance.canAttack = false;
        GameManager.Instance.canMove = false;
        //AudioManager.Instance.Play("InteractShop");
        Debug.Log("Shop");
    }
}
