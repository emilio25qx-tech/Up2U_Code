using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    public bool IsNoteOpen { get; private set; } = false;
    public bool IsMenuOpen { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool IsPaused => IsMenuOpen || IsNoteOpen;

    public void PauseNote()
    {
        IsNoteOpen = true;
        Time.timeScale = 0f;
    }

    public void ResumeNote()
    {
        IsNoteOpen = false;
        if (!IsMenuOpen) Time.timeScale = 1f;
    }

    public void PauseMenu()
    {
        IsMenuOpen = true;
        Time.timeScale = 0f;
    }

    public void ResumeMenu()
    {
        IsMenuOpen = false;
        if (!IsNoteOpen) Time.timeScale = 1f;
    }
}
