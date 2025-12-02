using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class FadeInUI : MonoBehaviour
{
    [Header("FadeIn Setting")]
    public float fadeDuration = 1f;
    public float delayBeforeFadeOut = 1f;

    [Header("Massage")]
    public TMP_Text messageText;

    [Header("Audio FadeIn")]
    public AudioSource musicSource;
    public AudioClip startMusicClip;
    
    private Image fadeImage;
    private float timeElapsed = 0f;
    private float initialTimeScale;

    void Awake()
    {
        fadeImage = GetComponent<Image>();
        if (fadeImage == null)
        {
            enabled = false;
            return;
        }
        fadeImage.color = Color.black;
        initialTimeScale = Time.timeScale;
    }

    void Start()
    {
        if (musicSource != null && startMusicClip != null)
        {
            musicSource.clip = startMusicClip;
            musicSource.Play();
        }
        StartCoroutine(PrepareAndFadeOut());
    }

    IEnumerator PrepareAndFadeOut()
    {
        Time.timeScale = 0f;
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(delayBeforeFadeOut);

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }

        Color startColor = Color.black;
        Color endColor = Color.clear;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            fadeImage.color = Color.Lerp(startColor, endColor, alpha);
            yield return null;
        }

        fadeImage.color = endColor;

        Time.timeScale = initialTimeScale;
    }
}