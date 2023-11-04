using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStartConversation : MonoBehaviour
{
    [SerializeField] private DialogueTrigger trigger;

    bool completed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !completed)
        {
            trigger.StartDialogue();
            completed = true;
        }
    }
}
