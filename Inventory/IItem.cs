using UnityEngine;

public interface IItem
{
    string GetName();
    Sprite GetIcon();
    void OnCollect();
}
