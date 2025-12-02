using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class NoteController : MonoBehaviour, IInteractable
{
    public string interactTitle => "[E]";

    [Header("UI")]
    public GameObject noteCanvas;
    public TMP_Text noteTextUI;
    [TextArea] public string noteText;

    [Header("Key Setting")]
    public KeyCode closeKey = KeyCode.Q;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openClip;

    private bool isOpen = false;
    private PlayerLook playerLook;

    private List<AudioSource> otherAudioSources = new List<AudioSource>();

    private void Start()
    {
        playerLook = FindFirstObjectByType<PlayerLook>();

        AudioSource[] sources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (var src in sources)
        {
            if (src != audioSource)
                otherAudioSources.Add(src);
        }
    }

    public void Interact(Player player)
    {
        if (!isOpen) OpenNote();
    }

    private void OpenNote()
    {
        noteTextUI.text = noteText;
        noteCanvas.SetActive(true);
        isOpen = true;

        if (playerLook != null)
            playerLook.enabled = false;

        foreach (var src in otherAudioSources)
            src.Pause();

        if (audioSource != null && openClip != null)
            audioSource.PlayOneShot(openClip);

        PauseManager.Instance.PauseNote();
    }

    private void CloseNote()
    {
        noteCanvas.SetActive(false);
        isOpen = false;

        if (playerLook != null)
            playerLook.enabled = true;

        foreach (var src in otherAudioSources)
            src.UnPause();

        PauseManager.Instance.ResumeNote();
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(closeKey))
            CloseNote();
    }

    public void OnFocus() { }
    public void OnLoseFocus() { }
}
