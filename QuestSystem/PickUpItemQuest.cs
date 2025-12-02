using UnityEngine;

public class PickUpItemQuest : IQuest
{
    private QuestData data;
    private PickUpItemCondition condition;

    private float timer = 0f;
    private string currentHint;

    public string Objective => data != null ? data.objectiveText : "ËÂÔºäÍà·çÁ¹Õé";
    public string CurrentHint => currentHint;
    public bool IsCompleted => condition != null && condition.IsCompleted();

    public PickUpItemQuest(QuestData data, PickUpItemCondition condition)
    {
        this.data = data;
        this.condition = condition;
        currentHint = "";
    }

    public void StartQuest()
    {
        timer = 0f;
        currentHint = "";
    }

    public void UpdateQuest()
    {
        if (IsCompleted) return;

        timer += Time.deltaTime;

        if (data != null && timer >= data.hintDelay)
        {
            currentHint = data.hintText;
        }
    }
}

