using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    public Camera cam;
    public LayerMask interactLayer;
    public KeyCode interactKey = KeyCode.E;
    public TextMeshProUGUI tooltipText;
    public Player player;
    public float range = 3f;
    public float interactionCooldown = 1f;
    private bool canInteract = true;
    private IInteractable currentTarget;

    private void Update()
    {
        HandleRaycast();
        HandleInput();
    }

    private void HandleRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (currentTarget != interactable)
                {
                    if (currentTarget != null) currentTarget.OnLoseFocus();
                    currentTarget = interactable;
                    currentTarget.OnFocus();
                }
                tooltipText.text = currentTarget.interactTitle;
                tooltipText.gameObject.SetActive(true);
                return;
            }
        }

        if (currentTarget != null)
        {
            currentTarget.OnLoseFocus();
            currentTarget = null;
            tooltipText.gameObject.SetActive(false);
        }
    }

    private void HandleInput()
    {
        if (currentTarget == null || !canInteract || PauseManager.Instance.IsPaused) return;

        if (Input.GetKeyDown(interactKey))
        {
            currentTarget.Interact(player);
            StartCoroutine(InteractionCooldown());
        }
    }

    private IEnumerator InteractionCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactionCooldown);
        canInteract = true;
    }
}
