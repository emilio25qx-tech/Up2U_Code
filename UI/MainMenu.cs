using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject areYouSurePanel;
    public GameObject settingsPanel;
    public GameObject loadingPanel;

    [Header("Loading Screen Setting")]
    public Slider loadingBar;
    public TMP_Text loadPromptText;
    public bool waitForInput = false;
    public KeyCode userPromptKey = KeyCode.Space;
    public float minimumLoadingTime = 2f;

    private float loadStartTime;

    public void Start()
    {
        mainMenuPanel.SetActive(true);
        areYouSurePanel.SetActive(false);
        settingsPanel.SetActive(false);
        loadingPanel.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            loadStartTime = Time.time;
            StartCoroutine(LoadAsynchronously(sceneName));
        }
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = !waitForInput;

        mainMenuPanel.SetActive(false);
        loadingPanel.SetActive(true);

        while (!operation.isDone || Time.time - loadStartTime < minimumLoadingTime)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;

            if (loadPromptText != null)
            {
                if (operation.progress >= 0.9f && waitForInput)
                {
                    loadPromptText.text = "Press " + userPromptKey.ToString().ToUpper() + " to continue";
                    if (Input.GetKeyDown(userPromptKey))
                    {
                        operation.allowSceneActivation = true;
                        loadPromptText.text = "";
                    }
                }
                else if (operation.progress >= 0.9f && !waitForInput)
                {
                    loadPromptText.text = "Loading....";
                    if (Time.time - loadStartTime >= minimumLoadingTime)
                    {
                        operation.allowSceneActivation = true;
                    }
                }
                else
                {
                    loadPromptText.text = "Loading.... (" + Mathf.RoundToInt(progress * 100f) + "%)";
                }
            }

            yield return null;
        }

        if (!waitForInput)
        {
            loadingPanel.SetActive(false);
        }
    }

    public void PlayGame()
    {
        LoadScene("HorrorGame");
    }

    public void ShowSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
        areYouSurePanel.SetActive(false);
        loadingPanel.SetActive(false);
    }

    public void HideSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        loadingPanel.SetActive(false);
    }

    public void ShowAreYouSure()
    {
        mainMenuPanel.SetActive(false);
        areYouSurePanel.SetActive(true);
        settingsPanel.SetActive(false);
        loadingPanel.SetActive(false);
    }

    public void OnYesQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnNoQuit()
    {
        areYouSurePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        loadingPanel.SetActive(false);
    }
}