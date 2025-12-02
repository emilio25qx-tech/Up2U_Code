using UnityEngine;

public class Radio : MonoBehaviour, IInteractable
{
    public string interactTitle => "[E]";
    public GameObject uiText;

    private AudioSource audioSrc;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        if (uiText != null) uiText.SetActive(false);
    }

    public void OnFocus()
    {
        if (uiText != null)
        {
            uiText.SetActive(true);
            uiText.GetComponent<TMPro.TMP_Text>().text = interactTitle;
        }
    }

    public void OnLoseFocus()
    {
        if (uiText != null) uiText.SetActive(false);
    }

    public void Interact(Player player)
    {
        if (audioSrc.isPlaying)
            audioSrc.Pause();
        else
            audioSrc.Play();
    }
}
