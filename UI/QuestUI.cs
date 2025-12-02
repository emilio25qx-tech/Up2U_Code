using UnityEngine;
using TMPro;
using System.Collections;

public class QuestUI : MonoBehaviour
{
    [Header("Text UI")]
    public TMP_Text objectiveText;
    public TMP_Text hintText;

    [Header("Fade Settings")]
    public float fadeDuration = 0.5f;

    private void Start()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.OnQuestUpdated += UpdateHint;
            QuestManager.Instance.OnQuestStarted += ShowObjective;
            QuestManager.Instance.OnQuestCompleted += ClearQuestUI;
        }

        SetAlpha(objectiveText, 0f);
        SetAlpha(hintText, 0f);
    }

    private void OnDestroy()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.OnQuestUpdated -= UpdateHint;
            QuestManager.Instance.OnQuestStarted -= ShowObjective;
            QuestManager.Instance.OnQuestCompleted -= ClearQuestUI;
        }
    }

    public void ShowObjective(IQuest quest)
    {
        StartCoroutine(FadeOutThenShowNewQuest(quest));
    }

    private IEnumerator FadeOutThenShowNewQuest(IQuest quest)
    {
        float objAlpha = objectiveText != null ? objectiveText.color.a : 0f;
        float hintAlpha = hintText != null ? hintText.color.a : 0f;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            if (objectiveText != null) objectiveText.color = new Color(objectiveText.color.r, objectiveText.color.g, objectiveText.color.b, a * objAlpha);
            if (hintText != null) hintText.color = new Color(hintText.color.r, hintText.color.g, hintText.color.b, a * hintAlpha);
            yield return null;
        }

        if (objectiveText != null) SetAlpha(objectiveText, 0f);
        if (hintText != null) SetAlpha(hintText, 0f);

        if (objectiveText != null)
        {
            objectiveText.text = quest.Objective;
            StartCoroutine(FadeText(objectiveText, 0f, 1f, fadeDuration));
        }
    }

    private void UpdateHint(string newHint)
    {
        if (hintText != null && !string.IsNullOrEmpty(newHint))
        {
            hintText.text = newHint;
            StartCoroutine(FadeText(hintText, 0f, 1f, fadeDuration));
        }
    }

    private void ClearQuestUI()
    {
        if (objectiveText != null)
            StartCoroutine(FadeText(objectiveText, objectiveText.color.a, 0f, fadeDuration));
        if (hintText != null)
            StartCoroutine(FadeText(hintText, hintText.color.a, 0f, fadeDuration));
    }

    private IEnumerator FadeText(TMP_Text text, float startAlpha, float endAlpha, float duration)
    {
        Color c = text.color;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float a = Mathf.Lerp(startAlpha, endAlpha, t / duration);
            text.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }
        text.color = new Color(c.r, c.g, c.b, endAlpha);
    }

    private void SetAlpha(TMP_Text text, float alpha)
    {
        if (text != null)
        {
            Color c = text.color;
            text.color = new Color(c.r, c.g, c.b, alpha);
        }
    }
}
