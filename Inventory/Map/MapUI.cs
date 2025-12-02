using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    public GameObject mapPanel;
    public Image mapImage;

    private bool isOpen = false;
    private int currentFloorIndex = 0;
    private List<int> unlockedFloors = new List<int>();

    void Start()
    {
        if (mapPanel != null) mapPanel.SetActive(false);
    }

    void Update()
    {
        if (PauseManager.Instance.IsNoteOpen || PauseManager.Instance.IsMenuOpen)
            return;

        if (Input.GetKeyDown(KeyCode.Space)) ToggleMap();

        if (isOpen && unlockedFloors.Count > 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) ChangeFloor(-1);
            if (Input.GetKeyDown(KeyCode.RightArrow)) ChangeFloor(1);
        }
    }

    void ToggleMap()
    {
        unlockedFloors = MapManager.Instance.GetUnlockedFloors();

        if (unlockedFloors.Count == 0)
        {
            Debug.Log("ยังไม่มีชั้นไหนรวมแผนที่ได้!");
            return;
        }

        isOpen = !isOpen;
        if (mapPanel != null) mapPanel.SetActive(isOpen);

        if (isOpen)
        {
            currentFloorIndex = 0;
            ShowFloor();
        }
    }

    void ChangeFloor(int direction)
    {
        currentFloorIndex += direction;
        currentFloorIndex = Mathf.Clamp(currentFloorIndex, 0, unlockedFloors.Count - 1);
        ShowFloor();
    }

    void ShowFloor()
    {
        if (unlockedFloors.Count == 0) return;

        int floor = unlockedFloors[currentFloorIndex];
        Sprite spr = MapManager.Instance.GetMapSprite(floor);
        if (mapImage != null) mapImage.sprite = spr;

        Debug.Log("แสดงแผนที่ชั้น: " + floor);
    }
}
