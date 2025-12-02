using UnityEngine;

[CreateAssetMenu(fileName = "QuestData", menuName = "Game/Quest Data")]
public class QuestData : ScriptableObject
{
    [Header("ข้อความเควส (Objective)")]
    [TextArea(2, 4)]
    public string objectiveText = "ให้สำรวจบ้าน";

    [Header("ข้อความ Hint ถ้าผู้เล่นช้า")]
    [TextArea(2, 4)]
    public string hintText = "ลองไปดูที่ตู้สิ…";

    [Header("เวลารอก่อนแสดง Hint (วินาที)")]
    public float hintDelay = 10f;
}
