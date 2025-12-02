using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public int totalFloors = 3;
    public int fragmentsPerFloor = 3;
    public Sprite[] fullMapSprites;

    [Header("Combine Settings")]
    public bool autoAddFullMapToInventory = true;
    public AudioClip combineClip;
    public AudioSource audioSource;

    private Dictionary<int, HashSet<int>> collectedFragments = new Dictionary<int, HashSet<int>>();
    private HashSet<int> unlockedFloors = new HashSet<int>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this.gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        for (int i = 1; i <= totalFloors; i++)
            collectedFragments[i] = new HashSet<int>();
    }

    public void AddFragment(int floor, int fragmentIndex)
    {
        if (!collectedFragments.ContainsKey(floor))
            collectedFragments[floor] = new HashSet<int>();

        if (collectedFragments[floor].Contains(fragmentIndex))
        {
            Debug.Log($"ชิ้นส่วน {fragmentIndex} ของชั้น {floor} ถูกเก็บแล้ว");
            return;
        }

        collectedFragments[floor].Add(fragmentIndex);

        Debug.Log($"เก็บชิ้นส่วนชั้น {floor} ชิ้น {fragmentIndex} = {collectedFragments[floor].Count}/{fragmentsPerFloor}");

        if (collectedFragments[floor].Count >= fragmentsPerFloor && !unlockedFloors.Contains(floor))
            UnlockFloor(floor);
    }

    void UnlockFloor(int floor)
    {
        unlockedFloors.Add(floor);
        Debug.Log($" รวมแผนที่ชั้น {floor} เรียบร้อย!");

        if (combineClip != null && audioSource != null) audioSource.PlayOneShot(combineClip);
        else if (combineClip != null && Camera.main != null) AudioSource.PlayClipAtPoint(combineClip, Camera.main.transform.position);

        RemoveFloorFragmentsFromInventory(floor);

        if (autoAddFullMapToInventory)
        {
            Sprite spr = GetMapSprite(floor);
            ItemData data = new ItemData($"Map F{floor}", spr, floor);
            InventorySystem.Instance?.AddItem(data);
        }
    }

    private void RemoveFloorFragmentsFromInventory(int floor)
    {
        if (InventorySystem.Instance == null) return;

        List<ItemData> toRemove = new List<ItemData>();
        foreach (var item in InventorySystem.Instance.GetItems())
        {
            if (item.floorNumber == floor && item.itemName.Contains("Fragment"))
                toRemove.Add(item);
        }

        foreach (var item in toRemove)
            InventorySystem.Instance.RemoveItem(item);
    }

    public bool IsFloorUnlocked(int floor) => unlockedFloors.Contains(floor);

    public Sprite GetMapSprite(int floor)
    {
        if (!IsFloorUnlocked(floor)) return null;
        if (fullMapSprites == null || fullMapSprites.Length < floor) return null;
        return fullMapSprites[floor - 1];
    }

    public List<int> GetUnlockedFloors()
    {
        List<int> list = new List<int>(unlockedFloors);
        list.Sort();
        return list;
    }
}
