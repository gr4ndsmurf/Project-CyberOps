using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MorsManager : MonoBehaviour
{
    public PuzzleSlot[] puzzleSlots;

    bool onetime = false;

    [SerializeField] Animator morseLightAnim;

    [SerializeField] private GameObject MorseCodeCanvas;

    public int CompleteCheck = 0;

    [SerializeField] private GameObject door_Open;
    [SerializeField] private GameObject door_Closed;
    [SerializeField] private GameObject interactionObject;
    void Update()
    {
        if (!onetime)
        {
            bool checkComplete = AllSlotComplete();
            if (checkComplete)
            {
                Debug.Log("LEVEL COMPLETED!");
                //morseLightAnim.SetBool("isCompleted", true);
                MorseCodeCanvas.SetActive(false);
                Cursor.visible = false;
                GameManager.Instance.canAttack = true;
                GameManager.Instance.canMove = true;
                door_Open.SetActive(true);
                door_Closed.SetActive(false);
                interactionObject.GetComponent<BoxCollider2D>().enabled = false;
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
        Cursor.visible = false;
        GameManager.Instance.canAttack = true;
        GameManager.Instance.canMove = true;
    }
}
