using System.Collections;
using UnityEngine;

public class JSObject : MonoBehaviour
{
    [Header("Ghost")]
    public GameObject ghostOb;

    [Header("Ghost Setting")]
    public float displayDuration = 3f;
    public bool startHidden = true;
    public bool destroyAfterDisplay = false;
    public bool disableColliderAfterDisplay = true;

    [Header("Audio JumpScare")]
    public AudioSource audioSource;
    
    private Collider thisCollider;
    private Coroutine hideCoroutine;
    private bool hasPlayedSound = false;

    private void Start()
    {
        if (ghostOb != null)
        {
            ghostOb.gameObject.SetActive(!startHidden);
        }
        thisCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasPlayedSound)
            {
                if (ghostOb != null)
                {
                    ghostOb.gameObject.SetActive(true);
                }
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                    hasPlayedSound = true;
                }

                if (hideCoroutine != null)
                {
                    StopCoroutine(hideCoroutine);
                }
                hideCoroutine = StartCoroutine(HandleDisplayAndDestruction(displayDuration));

                if (disableColliderAfterDisplay)
                {
                    if (thisCollider != null)
                    {
                        thisCollider.enabled = false;
                    }
                }
            }
        }
    }

    private IEnumerator HandleDisplayAndDestruction(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (ghostOb != null)
        {
            ghostOb.gameObject.SetActive(false);
        }
        hideCoroutine = null;

        if (destroyAfterDisplay)
        {
            if (ghostOb != null)
            {
                Destroy(ghostOb);
            }
        }
    }
}