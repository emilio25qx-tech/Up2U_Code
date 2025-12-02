using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnterZoneCondition : MonoBehaviour, IQuestCondition
{
    [Header("Quest Data สำหรับโซนนี้ (ลาก QuestData ลงที่นี่)")]
    public QuestData questData;

    private bool triggered = false;

    public bool IsCompleted() => triggered;

    public bool IsPlayerEntered => triggered;

    private void Reset()
    {
        Collider c = GetComponent<Collider>();
        if (c != null) c.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            triggered = true;
    }
}
