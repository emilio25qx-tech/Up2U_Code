using UnityEngine;
using System.Collections.Generic;
using System;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private Queue<IQuest> quests = new Queue<IQuest>();
    private IQuest currentQuest;

    public Action<string> OnQuestUpdated;
    public Action<IQuest> OnQuestStarted;
    public Action OnQuestCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Update()
    {
        if (currentQuest == null) return;
        currentQuest.UpdateQuest();

        OnQuestUpdated?.Invoke(currentQuest.CurrentHint);

        if (currentQuest.IsCompleted)
            StartNextQuest();
    }

    public void AddQuest(IQuest quest)
    {
        quests.Enqueue(quest);
    }

    public void StartQuestLine()
    {
        StartNextQuest();
    }

    private void StartNextQuest()
    {
        if (quests.Count == 0)
        {
            currentQuest = null;
            OnQuestUpdated?.Invoke("");
            OnQuestCompleted?.Invoke();
            return;
        }

        currentQuest = quests.Dequeue();
        currentQuest.StartQuest();

        OnQuestStarted?.Invoke(currentQuest);
        OnQuestUpdated?.Invoke(currentQuest.CurrentHint);
    }

    public IQuest GetCurrentQuest()
    {
        return currentQuest;
    }
}
