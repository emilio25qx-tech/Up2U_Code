using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InteractWithAnimation : MonoBehaviour, IInteractable
{
    public string interactTitle => "Press E";

    [Header("Animation")]
    public string animationBoolName = "Open";
    private Animator targetAnimator;
    private bool isAnimationActive = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    [Header("Cooldown")]
    public float interactionCooldown = 1f;
    private bool canInteract = true;

    private void Awake()
    {
        targetAnimator = GetComponent<Animator>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Interact(Player player)
    {
        if (!canInteract) return;

        ToggleAnimation();
        StartCoroutine(InteractionCooldown());
    }

    private void ToggleAnimation()
    {
        isAnimationActive = !isAnimationActive;
        targetAnimator.SetBool(animationBoolName, isAnimationActive);

        if (audioSource != null)
        {
            if (isAnimationActive)
                audioSource.PlayOneShot(openSound);
            else
                audioSource.PlayOneShot(closeSound);
        }
    }

    private IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactionCooldown);
        canInteract = true;
    }

    public void OnFocus() { }
    public void OnLoseFocus() { }
}
