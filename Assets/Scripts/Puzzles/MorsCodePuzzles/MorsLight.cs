using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorsLight : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip completedClip;

    bool onetime = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayMorseCode()
    {
        audioSource.Play();
    }

    public void PlayCompletedSound()
    {
        if (!onetime)
        {
            audioSource.clip = completedClip;
            audioSource.Play();
            onetime = true;
        }
    }
}
