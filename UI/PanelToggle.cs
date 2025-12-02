using UnityEngine;
using UnityEngine.UI;

public class PanelToggle : MonoBehaviour
{
    public GameObject targetPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TogglePanel();
        }
    }

    private void TogglePanel()
    {
        if (targetPanel != null)
        {
            bool isActive = targetPanel.activeSelf;
            targetPanel.SetActive(!isActive);
        }
    }
}