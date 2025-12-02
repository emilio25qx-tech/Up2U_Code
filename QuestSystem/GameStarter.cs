using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public EnterZoneCondition livingRoomZone;
    public EnterZoneCondition kitchenZone;

    public QuestUI questUI;

    private void Start()
    {
        var q1 = new ExploreHouseQuest(livingRoomZone.questData, livingRoomZone);
        var q2 = new ExploreHouseQuest(kitchenZone.questData, kitchenZone);

        QuestManager.Instance.AddQuest(q1);
        QuestManager.Instance.AddQuest(q2);

        QuestManager.Instance.StartQuestLine();

        if (questUI != null)
            questUI.ShowObjective(q1);
    }
}
