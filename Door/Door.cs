using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string interactTitle => "[E]";

    [Header("Animator")]
    public string openTrigger = "open";
    public string closeTrigger = "close";

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openClip;
    public AudioClip closeClip;
    public AudioClip lockClip;

    [Header("Lock System")]
    public string requiredKeyName = "BathroomKey";
    public string doorTag = "DoorLock";

    [Header("Deletion Option")]
    public bool deleteOnInteract = false;
    public GameObject objectToDelete;

    private Animator anim;
    private bool isOpen = false;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Interact(Player player)
    {
        if (CompareTag(doorTag))
        {
            bool hasKey = InventorySystem.Instance.GetItems().Exists(item => item.itemName == requiredKeyName);
            if (!hasKey)
            {
                if (audioSource != null && lockClip != null)
                    audioSource.PlayOneShot(lockClip);

                Debug.Log($"ต้องมี {requiredKeyName} ก่อนเปิดประตูนี้!");
                return;
            }
        }

        anim.SetTrigger(isOpen ? closeTrigger : openTrigger);

        if (audioSource != null)
        {
            if (isOpen && closeClip != null) audioSource.PlayOneShot(closeClip);
            else if (!isOpen && openClip != null) audioSource.PlayOneShot(openClip);
        }

        isOpen = !isOpen;

        if (deleteOnInteract && objectToDelete != null)
        {
            Destroy(objectToDelete);
        }
    }

    public void OnFocus() { }
    public void OnLoseFocus() { }
}