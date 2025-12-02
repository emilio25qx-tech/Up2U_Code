using UnityEngine;
using TMPro;

public class PickUpKey : MonoBehaviour, IInteractable, IItem
{
    [Header("Key Info")]
    public string keyName = "BathroomKey";
    public Sprite icon;

    [Header("Audio")]
    public AudioClip pickupClip;        

    public string interactTitle => "[E]";

    public void Interact(Player player)
    {
        OnCollect();
    }

    public void OnFocus() { }
    public void OnLoseFocus() { }

    public string GetName() => keyName;
    public Sprite GetIcon() => icon;

    public void OnCollect()
    {
        if (pickupClip != null)
            AudioSource.PlayClipAtPoint(pickupClip, transform.position);

        ItemData data = new ItemData(GetName(), GetIcon(), 0);

        bool added = InventorySystem.Instance?.AddItem(data, playSound: false) ?? false;

        if (added)
        {
            gameObject.SetActive(false);
        }
    }
}
