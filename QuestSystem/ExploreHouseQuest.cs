using UnityEngine;

public class ExploreHouseQuest : IQuest
{
    private QuestData data;
    private EnterZoneCondition condition;

    private float timer = 0f;
    private bool hintShown = false;

    public string Objective => data != null ? data.objectiveText : "สำรวจพื้นที่นี้";
    public string CurrentHint { get; private set; } = "";

    public bool IsCompleted => condition != null && condition.IsCompleted();

    public ExploreHouseQuest(QuestData data, EnterZoneCondition condition)
    {
        this.data = data;
        this.condition = condition;
    }

    public void StartQuest()
    {
        timer = 0f;
        CurrentHint = "";
        hintShown = false;
    }

    public void UpdateQuest()
    {
        if (IsCompleted || hintShown) return;

        timer += Time.deltaTime;

        if (data != null && timer >= data.hintDelay)
        {
            CurrentHint = data.hintText;
            hintShown = true;
        }
    }
}
