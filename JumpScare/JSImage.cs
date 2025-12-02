using UnityEngine;
using UnityEngine.UI;

public class JSImage : MonoBehaviour
{
    [Header("Image")]
    public RawImage imageToShow;

    [Header("Audio JumpScare")]
    public AudioSource audioSource;
    private void Start()
    {
        imageToShow.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            imageToShow.gameObject.SetActive(true);
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            imageToShow.gameObject.SetActive(false);
        }
    }
}