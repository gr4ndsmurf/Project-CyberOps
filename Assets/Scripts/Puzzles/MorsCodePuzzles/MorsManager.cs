using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorsManager : MonoBehaviour
{
    public PuzzleSlot[] puzzleSlots;

    bool onetime = false;

    [SerializeField] Animator morseLightAnim;

    [SerializeField] private GameObject MorseCodeCanvas;

    public int CompleteCheck = 0;
    void Update()
    {
        if (!onetime)
        {
            bool checkComplete = AllSlotComplete();
            if (checkComplete)
            {
                Debug.Log("LEVEL COMPLETED!");
                //morseLightAnim.SetBool("isCompleted", true);
                CompleteCheck = 1;
                onetime = true;
            }
        }
        morseLightAnim.SetInteger("CompleteCheck", CompleteCheck);
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

    public void CloseCanvas()
    {
        MorseCodeCanvas.SetActive(false);
    }
}
