using UnityEngine;

public class ShowObAndDestroyOb : MonoBehaviour
{
    [Header("Target Object to Show")]
    public GameObject targetObject;
    public GameObject objectToDestroy;

    [Header("Settings")]
    public bool startHidden = true;
    public bool disableColliderAfterTrigger = true;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip audioClip;

    private Collider thisCollider;
    private bool hasTriggered = false;

    private void Start()
    {
        if (targetObject != null)
            targetObject.SetActive(!startHidden);

        thisCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            if (targetObject != null)
                targetObject.SetActive(true);

            if (audioSource != null && audioClip != null)
                audioSource.PlayOneShot(audioClip);

            if (objectToDestroy != null)
                Destroy(objectToDestroy);

            if (disableColliderAfterTrigger && thisCollider != null)
                thisCollider.enabled = false;
        }
    }
}
