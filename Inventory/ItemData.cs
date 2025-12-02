using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public Sprite icon;
    public int floorNumber;

    public ItemData(string name, Sprite iconSprite, int floor)
    {
        itemName = name;
        icon = iconSprite;
        floorNumber = floor;
    }
}
