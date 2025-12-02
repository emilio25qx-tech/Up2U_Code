using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject areYouSurePanel;
    public GameObject crosshair;

    private Player player;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();

        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        areYouSurePanel.SetActive(false);
        crosshair.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseManager.Instance.IsNoteOpen) return;

            if (!PauseManager.Instance.IsMenuOpen)
                OpenMenu();
            else
                CloseMenu();
        }
    }

    private void OpenMenu()
    {
        PauseManager.Instance.PauseMenu();
        mainMenuPanel.SetActive(true);
        crosshair.SetActive(false);

        if (player != null) player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        AudioListener.pause = true;
    }

    private void CloseMenu()
    {
        PauseManager.Instance.ResumeMenu();
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        areYouSurePanel.SetActive(false);
        crosshair.SetActive(true);

        if (player != null) player.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        AudioListener.pause = false;
    }


    public void ContinueGame() => CloseMenu();
    public void ShowSettings() { mainMenuPanel.SetActive(false); settingsPanel.SetActive(true); }
    public void HideSettings() { settingsPanel.SetActive(false); mainMenuPanel.SetActive(true); }
    public void ShowAreYouSure() { mainMenuPanel.SetActive(false); settingsPanel.SetActive(false); areYouSurePanel.SetActive(true); }
    public void OnYesBack() { PauseManager.Instance.ResumeMenu(); SceneManager.LoadScene("MainMenu"); }
    public void OnNoQuit() { areYouSurePanel.SetActive(false); mainMenuPanel.SetActive(true); }
}
