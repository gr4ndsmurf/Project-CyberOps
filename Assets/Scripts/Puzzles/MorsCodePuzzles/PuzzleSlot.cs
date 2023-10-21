using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    [SerializeField] private int neededID;

    public bool cardCheck;

    [SerializeField] private MorsCard cardInSlot;

    [SerializeField] private int cardID_inSlot;

    private void Update()
    {
        if (transform.childCount != 0)
        {
            cardInSlot = GetComponentInChildren<MorsCard>();
            cardID_inSlot = cardInSlot.cardID;

            if (cardID_inSlot == neededID)
            {
                cardCheck = true;
            }
            else
            {
                cardCheck = false;
            }
        }
    }
}
