using UnityEngine;
using TMPro;

public class PlayerInteractionUIDialogue : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI tooltipText;
    public float dialogueRange = 3f;
    public LayerMask dialogueLayer;
    public Player player;

    private Camera playerCamera;
    private DialogueTrigger currentTrigger;

    void Start()
    {
        playerCamera = Camera.main;
        tooltipText.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckForTooltip();
        CheckForInteraction();
    }

    void CheckForTooltip()
    {
        tooltipText.gameObject.SetActive(false);
        currentTrigger = null;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, dialogueRange, dialogueLayer))
        {
            DialogueTrigger trigger = hit.collider.GetComponent<DialogueTrigger>();

            if (trigger != null && !trigger.autoTrigger)
            {
                tooltipText.gameObject.SetActive(true);
                currentTrigger = trigger;
            }
        }
    }

    void CheckForInteraction()
    {
        if (currentTrigger == null) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentTrigger.TryTriggerByE(player);
        }
    }
}
