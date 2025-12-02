using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LookSpeedSlider : MonoBehaviour
{
    [Header("LookSpeedUI Setting")]
    public Slider lookSpeedSlider;
    public TextMeshProUGUI lookSpeedText;
    public PlayerLook playerLook;

    private const string LookSpeedKey = "LookSpeed";

    void Start()
    {
        float savedSpeed = PlayerPrefs.GetFloat(LookSpeedKey, playerLook.lookSpeed);
        playerLook.lookSpeed = savedSpeed;

        lookSpeedSlider.value = savedSpeed;
        UpdateLookSpeedText(savedSpeed);

        lookSpeedSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float newSpeed)
    {
        playerLook.lookSpeed = newSpeed;
        UpdateLookSpeedText(newSpeed);

        PlayerPrefs.SetFloat(LookSpeedKey, newSpeed);
        PlayerPrefs.Save();
    }

    void UpdateLookSpeedText(float value)
    {
        lookSpeedText.text = $"{value:F1}";
    }
}
