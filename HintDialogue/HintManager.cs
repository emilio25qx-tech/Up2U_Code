using System.Collections;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance { get; private set; }

    [Header("Options")]
    [Tooltip("รอหลังจากแสดง hint ก่อนให้สามารถแสดง hint ใหม่ได้")]
    public float cooldown = 5f;

    private bool canShowHint = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowHint(HintLineData hint)
    {
        if (canShowHint)
        {
            // ใช้ DialogueManager แสดง hint
            DialogueData tempDialogue = new DialogueData();
            tempDialogue.lines = new DialogueLine[1];
            tempDialogue.lines[0] = new DialogueLine
            {
                speakerName = hint.speakerName,
                message = hint.message
            };

            DialogueManager.Instance.StartDialogue(tempDialogue);

            canShowHint = false;
            StartCoroutine(ResetCooldown());
        }
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canShowHint = true;
    }
}
