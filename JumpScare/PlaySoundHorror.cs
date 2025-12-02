using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundHorror : MonoBehaviour
{
    [Header("Audio")]
    public GameObject playHorrorSound;
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            Destroy(playHorrorSound);
        }
    }
}
