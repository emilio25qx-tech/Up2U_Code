using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    public string interactTitle => isOpened ? "" : "[E]";

    [Header("Animator")]
    public string openTrigger = "open";

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openClip;
    public AudioClip lockClip;

    [Header("Lock System")]
    public string requiredKeyName = "BoxKey";
    public string boxTag = "BoxLock";

    private Animator anim;
    private bool isOpened = false;
    private Collider boxCollider;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();

        boxCollider = GetComponent<Collider>();
        if (boxCollider == null)
        {
            boxCollider = GetComponentInParent<Collider>();
        }

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Interact(Player player)
    {
        if (isOpened)
        {
            return;
        }

        if (CompareTag(boxTag))
        {
            bool hasKey = InventorySystem.Instance.GetItems().Exists(item => item.itemName == requiredKeyName);

            if (!hasKey)
            {
                if (audioSource != null && lockClip != null)
                    audioSource.PlayOneShot(lockClip);

                return;
            }
        }

        if (anim != null)
        {
            anim.SetTrigger(openTrigger);
        }

        if (audioSource != null && openClip != null)
        {
            audioSource.PlayOneShot(openClip);
        }

        isOpened = true;

        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

    }

    public void OnFocus() { }
    public void OnLoseFocus() { }
}