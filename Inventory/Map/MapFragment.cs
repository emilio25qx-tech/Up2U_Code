using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MapFragment : MonoBehaviour, IItem, IInteractable
{
    [Header("Map Fragment")]
    public string fragmentName = "Map Fragment";
    public Sprite icon;
    public int floorNumber = 1;

    [Tooltip("ลำดับของชิ้นส่วนในชั้น เช่น 1,2,3")]
    public int fragmentIndex = 1;

    public AudioClip pickupClip;

    public string interactTitle => "[E]";

    public void OnFocus() { }
    public void OnLoseFocus() { }

    public void Interact(Player player)
    {
        OnCollect();
    }

    public string GetName() => $"{fragmentName}";
    public Sprite GetIcon() => icon;

    public void OnCollect()
    {
        if (pickupClip != null)
        {
            if (InventorySystem.Instance?.sfxSource != null)
                InventorySystem.Instance.sfxSource.PlayOneShot(pickupClip);
            else if (Camera.main != null)
                AudioSource.PlayClipAtPoint(pickupClip, Camera.main.transform.position);
        }

        ItemData data = new ItemData(GetName(), GetIcon(), floorNumber);

        bool added = InventorySystem.Instance?.AddItem(data, playSound: false) ?? false;

        if (added)
        {
            MapManager.Instance?.AddFragment(floorNumber, fragmentIndex);

            Destroy(gameObject);
        }
    }

    private void Reset()
    {
        Collider c = GetComponent<Collider>();
        if (c != null) c.isTrigger = true;
    }
}
