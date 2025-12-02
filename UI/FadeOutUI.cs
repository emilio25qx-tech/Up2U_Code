using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FadeOutUI : MonoBehaviour
{
    [Header("FadeOut Setting")]
    public GameObject uiFadePanel;   
    public float fadeDurationPanel = 1f;
    public float fadeDurationText = 1f;
    public float panelDisplayDuration = 2f;
    
    [Header("Massage")]
    public TMP_Text uiMessageText;

    [Header("Audio FadeOut")]
    public AudioSource musicSource;
    public AudioClip music;
    public AudioSource drivecarSource;
    public AudioClip drivecarSound;

    [Header("Scene Setting")]
    public string nextSceneName;

    private Image fadeImage;
    private Color targetColorPanel = Color.black;
    private Color targetColorText;
    private bool drivecarPlayed = false;

    void Start()
    {
        if (uiFadePanel == null || uiMessageText == null)
        {
            enabled = false;
            return;
        }
        if (musicSource == null)
        {
            enabled = false;
            return;
        }
        if (drivecarSource == null)
        {
            enabled = false;
            return;
        }

        fadeImage = uiFadePanel.GetComponent<Image>();
        Color startColorPanel = fadeImage.color;
        startColorPanel.a = 0f;
        fadeImage.color = startColorPanel;
        uiFadePanel.SetActive(false);

        Color startColorText = uiMessageText.color;
        startColorText.a = 0f;
        uiMessageText.color = startColorText;
        uiMessageText.gameObject.SetActive(false);

        targetColorPanel.a = 1f;
        targetColorText = uiMessageText.color;
        targetColorText.a = 1f;
    }

    public void FadeOut(string message)
    {
        if (uiFadePanel != null && uiMessageText != null)
        {
            uiFadePanel.SetActive(true);
            uiMessageText.gameObject.SetActive(true);
            uiMessageText.text = message;
            StartCoroutine(FadeOutAndChangeScene());
        }
    }

    IEnumerator FadeOutAndChangeScene()
    {
        if (drivecarSource != null && drivecarSound != null && !drivecarPlayed)
        {
            drivecarSource.PlayOneShot(drivecarSound);
            drivecarPlayed = true;
        }

        if (musicSource != null && music != null)
        {
            musicSource.PlayOneShot(music);
        }

        float timerPanel = 0f;
        Color currentPanelColor = fadeImage.color;
        float timerText = 0f;
        Color currentTextColor = uiMessageText.color;

        while (timerPanel < fadeDurationPanel)
        {
            timerPanel += Time.deltaTime;
            float alphaPanel = Mathf.Lerp(0f, 1f, timerPanel / fadeDurationPanel);
            currentPanelColor.a = alphaPanel;
            fadeImage.color = currentPanelColor;

            if (timerPanel >= (1f - (fadeDurationText / fadeDurationPanel)) && timerText < fadeDurationText)
            {
                timerText += Time.deltaTime;
                float alphaText = Mathf.Lerp(0f, 1f, timerText / fadeDurationText);
                currentTextColor.a = alphaText;
                uiMessageText.color = currentTextColor;
            }
            yield return null;
        }

        currentPanelColor.a = 1f;
        fadeImage.color = currentPanelColor;
        currentTextColor.a = 1f;
        uiMessageText.color = currentTextColor;

        yield return new WaitForSeconds(panelDisplayDuration);

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(nextSceneName);
        }
    }
}