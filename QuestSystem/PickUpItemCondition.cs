using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickUpItemCondition : MonoBehaviour, IQuestCondition
{
    [Header("Quest Data สำหรับ item นี้")]
    public QuestData questData;

    private bool pickedUp = false;

    public bool IsCompleted() => pickedUp;

    private void Reset()
    {
        Collider c = GetComponent<Collider>();
        if (c != null) c.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickedUp = true;

            gameObject.SetActive(false);
        }
    }
}
