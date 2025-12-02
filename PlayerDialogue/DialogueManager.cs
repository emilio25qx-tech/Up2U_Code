using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue Settings")]
    [Tooltip("ความเร็วการพิมพ์ตัวอักษรทีละตัว")]
    public float typingSpeed = 0.03f;

    [Tooltip("เวลารอหลังพิมพ์เสร็จ ก่อนขึ้นข้อความถัดไปอัตโนมัติ")]
    public float autoNextDelay = 1f;

    private bool autoNextEnabled = true;

    private DialogueLine[] currentLines;
    private int currentIndex = 0;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueData data)
    {
        currentLines = data.lines;
        currentIndex = 0;

        dialoguePanel.SetActive(true);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (currentIndex >= currentLines.Length)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = currentLines[currentIndex];
        currentIndex++;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText($"{line.speakerName} : {line.message}"));
    }

    public void StartDialogueLine(DialogueLine line)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLineWithoutCooldown(line));
    }

    private IEnumerator TypeText(string fullText)
    {
        dialogueText.text = "";

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (autoNextEnabled)
        {
            yield return new WaitForSeconds(autoNextDelay);
            ShowNextLine();
        }
    }

    private IEnumerator TypeLineWithoutCooldown(DialogueLine line)
    {
        dialogueText.text = "";

        foreach (char c in $"{line.speakerName} : {line.message}")
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        dialogueText.text = "";
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentLines = null;
        currentIndex = 0;
    }
}
