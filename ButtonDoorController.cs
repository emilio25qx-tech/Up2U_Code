using System.Collections;
using UnityEngine;

public class ButtonDoorController : MonoBehaviour, IInteractable
{
    // interface property
    public string interactTitle => "Press E to Open Door";

    [Header("Animation & Audio")]
    public Animator doorAnim;
    public string openAnimationName = "DoorOpen";
    public AudioSource audioSource;
    public AudioClip doorOpenSound;

    [Header("Interaction Settings")]
    public float interactionCooldown = 1f; // ป้องกัน spam

    private bool isOpen = false;
    private bool canInteract = true;

    public void OnFocus()
    {
        // ใส่ highlight ถ้าต้องการ
    }

    public void OnLoseFocus()
    {
        // ปิด highlight
    }

    public void Interact(Player player)
    {
        if (!canInteract) return;

        if (!isOpen)
        {
            doorAnim.Play(openAnimationName, 0, 0f);
            PlaySound(doorOpenSound);
            isOpen = true;
        }

        player.StartCoroutine(InteractionCooldown());
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactionCooldown);
        canInteract = true;
    }
}
