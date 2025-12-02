using UnityEngine;

public class CarDoorInteractEnd : MonoBehaviour, IInteractable
{
    [Header("Interaction")]
    [SerializeField] private string _interactTitle = "[E]";
    public string sceneToLoad = "YourSceneName";
    public string fadeMessage = "";

    private FadeOutUI fadeUI;
    private bool hasInteracted = false;

    [Header("Player Script")]
    public MonoBehaviour playerScriptToDisable;

    public string interactTitle => _interactTitle;

    void Start()
    {
        fadeUI = FindAnyObjectByType<FadeOutUI>();
    }

    public void OnFocus() { }
    public void OnLoseFocus() { }

    public void Interact(Player player)
    {
        if (hasInteracted) return;
        hasInteracted = true;

        if (playerScriptToDisable != null)
        {
            playerScriptToDisable.enabled = false;
        }

        if (fadeUI != null)
        {
            fadeUI.nextSceneName = sceneToLoad;
            fadeUI.FadeOut(fadeMessage);
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        this.enabled = false;
    }
}
