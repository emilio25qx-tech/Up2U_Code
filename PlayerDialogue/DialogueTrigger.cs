using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour, IDialogueTrigger, IInteractable
{
    [Header("Dialogue Settings")]
    public DialogueData dialogue;

    [Tooltip("ถ้าเปิด: Player เดินชนแล้วจะเล่น Dialogue อัตโนมัติ (Collider IsTrigger = true) \nถ้าปิด: Player ต้องกด E (Collider IsTrigger = false)")]
    public bool autoTrigger = false;

    [Header("Trigger Options")]
    public bool playOnce = false;
    public float eCooldown = 2f;

    [Header("Hint Settings")]
    public bool isHint = false;
    public float hintDelay = 20f;

    [Header("Destroy Options")]
    public bool destroyAfterTrigger = false;

    private bool canTriggerE = true;
    private bool hasPlayed = false;

    private Collider playerCollider;
    private Coroutine hintCoroutine;

    public string interactTitle => "[E]";

    public void OnFocus() { }
    public void OnLoseFocus() { }

    public void Interact(Player player)
    {
        TryTriggerByE(player);
    }

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = autoTrigger || isHint;
    }

    #region Dialogue Trigger
    public void TriggerDialogue(Player player)
    {
        if (playOnce && hasPlayed) return;

        DialogueManager.Instance.StartDialogue(dialogue);
        hasPlayed = true;

        if (!autoTrigger) StartCoroutine(ECooldown());

        if (destroyAfterTrigger)
        {
            Destroy(gameObject);
        }
    }

    public void TryTriggerByE(Player player)
    {
        if (!autoTrigger && canTriggerE)
        {
            TriggerDialogue(player);
        }
    }

    private IEnumerator ECooldown()
    {
        canTriggerE = false;
        yield return new WaitForSeconds(eCooldown);
        canTriggerE = true;
    }
    #endregion

    #region Auto Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (autoTrigger && other.CompareTag("Player"))
        {
            playerCollider = other;
            TriggerDialogue(other.GetComponent<Player>());
        }

        if (isHint && other.CompareTag("Player"))
        {
            if (hintCoroutine != null) StopCoroutine(hintCoroutine);
            hintCoroutine = StartCoroutine(HintRoutine(other.GetComponent<Player>()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isHint && other.CompareTag("Player"))
        {
            if (hintCoroutine != null)
            {
                StopCoroutine(hintCoroutine);
                hintCoroutine = null;
            }
        }
    }

    private IEnumerator HintRoutine(Player player)
    {
        yield return new WaitForSeconds(hintDelay);
        DialogueManager.Instance.StartDialogue(dialogue);
        hintCoroutine = null;

        if (destroyAfterTrigger)
            Destroy(gameObject);
    }
    #endregion
}
