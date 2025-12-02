using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemSlot : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;

    private ItemData currentData;

    public void SetItemData(ItemData data)
    {
        currentData = data;
        if (iconImage != null) iconImage.sprite = data.icon;
        if (nameText != null) nameText.text = data.itemName;
    }

    public void OnClick()
    {
        Debug.Log("Clicked item: " + (currentData != null ? currentData.itemName : "null"));
    }
}
