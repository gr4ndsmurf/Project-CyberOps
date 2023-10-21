using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorsManager : MonoBehaviour
{
    public PuzzleSlot[] puzzleSlots;

    bool onetime = false;
    void Update()
    {
        if (!onetime)
        {
            bool checkComplete = AllSlotComplete();
            if (checkComplete)
            {
                Debug.Log("LEVEL COMPLETED!");
                onetime = true;
            }
        }
    }

    public bool AllSlotComplete()
    {
        for (int i = 0; i < puzzleSlots.Length; i++)
        {
            if (puzzleSlots[i].cardCheck == false)
            {
                return false;
            }
        }
        return true;
    }
}
