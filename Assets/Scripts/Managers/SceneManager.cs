using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startMusic());
    }

    IEnumerator startMusic()
    {
        yield return new WaitForSeconds(0.05f);
        AudioManager.Instance.Play("musicPlaylist");
    }
}
