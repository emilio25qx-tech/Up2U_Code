using UnityEngine;
public interface IQuest
{
    string Objective { get; }
    string CurrentHint { get; }
    bool IsCompleted { get; }
    void StartQuest();
    void UpdateQuest();
}



