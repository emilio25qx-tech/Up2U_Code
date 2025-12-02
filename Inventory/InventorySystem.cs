using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    [Header("Inventory UI")]
    public GameObject inventoryPanel;
    public Transform gridParent;
    public GameObject itemSlotPrefab;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioClip inventoryClip;

    [Header("Settings")]
    [Tooltip("จำนวนสูงสุดของไอเท็มใน inventory")]
    public int maxItems = 20;

    [Header("UI Warning")]
    public TextMeshProUGUI warningText;
    public float warningDuration = 2f;

    private bool isOpen = false;
    private List<ItemData> items = new List<ItemData>();
    private Coroutine warningCoroutine;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this.gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
        if (warningText != null) warningText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleInventory();
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        if (inventoryPanel != null) inventoryPanel.SetActive(isOpen);
        if (isOpen) RefreshUI();

        if (sfxSource != null && inventoryClip != null)
            sfxSource.PlayOneShot(inventoryClip);
    }

    public bool AddItem(ItemData data, bool playSound = true)
    {
        if (data == null) return false;

        if (items.Count >= maxItems)
        {
            ShowWarning("กระเป๋าเต็ม!");
            return false; // ไม่เพิ่มไอเท็ม
        }

        items.Add(data);
        Debug.Log($"Picked up: {data.itemName} (Floor {data.floorNumber})");

        if (playSound)
            PlayPickupSound(null);

        if (inventoryPanel != null && inventoryPanel.activeSelf) RefreshUI();
        return true;
    }

    private void ShowWarning(string message)
    {
        if (warningText == null) return;

        if (warningCoroutine != null)
            StopCoroutine(warningCoroutine);

        warningCoroutine = StartCoroutine(WarningRoutine(message));
    }

    private System.Collections.IEnumerator WarningRoutine(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(warningDuration);
        warningText.gameObject.SetActive(false);
    }

    public void PlayPickupSound(AudioClip clip)
    {
        if (sfxSource != null) sfxSource.PlayOneShot(clip ?? inventoryClip);
        else if (clip != null && Camera.main != null) AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    void RefreshUI()
    {
        if (gridParent == null || itemSlotPrefab == null) return;

        for (int i = gridParent.childCount - 1; i >= 0; i--)
            Destroy(gridParent.GetChild(i).gameObject);

        foreach (var it in items)
        {
            GameObject go = Instantiate(itemSlotPrefab, gridParent);
            InventoryItemSlot slot = go.GetComponent<InventoryItemSlot>();
            if (slot != null) slot.SetItemData(it);
        }
    }

    public List<ItemData> GetItems() => new List<ItemData>(items);

    public void RemoveItem(ItemData data)
    {
        if (data == null) return;
        items.Remove(data);
        if (inventoryPanel != null && inventoryPanel.activeSelf) RefreshUI();
    }
}
