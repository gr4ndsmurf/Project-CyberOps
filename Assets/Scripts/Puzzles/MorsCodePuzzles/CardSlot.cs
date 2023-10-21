using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            //Debug.Log("DROPPED");
            GameObject dropped = eventData.pointerDrag;
            MorsCard draggableItem = dropped.GetComponent<MorsCard>();
            draggableItem.parentAfterDrag = transform;

        }
        else //swap items
        {
            //Debug.Log("Swap items)");

            GameObject dropped = eventData.pointerDrag;
            MorsCard draggableItem = dropped.GetComponent<MorsCard>();
            Transform originalParent = draggableItem.parentAfterDrag;

            // Swap
            Transform itemInSlot = transform.GetChild(0);
            draggableItem.parentAfterDrag = transform;
            itemInSlot.SetParent(originalParent);
            dropped.transform.SetParent(transform);
            itemInSlot.SetAsLastSibling();

        }
    }
}
