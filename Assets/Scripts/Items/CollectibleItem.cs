using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private int value = 750;

    public enum ItemType { Money, Card }
    public ItemType CurrentType = ItemType.Money;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (CurrentType)
            {
                case ItemType.Money:
                    GameManager.Instance.money += value;
                    
                    break;
                case ItemType.Card:
                    GameManager.Instance.cards += value;
                    
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
