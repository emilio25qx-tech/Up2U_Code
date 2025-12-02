using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float drainRate = 15f;
    public float recoverRate = 10f;
    public Slider staminaSlider;

    public float currentStamina;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip heavyBreathClip;

    private PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        currentStamina = maxStamina;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        if (audioSource != null && heavyBreathClip != null)
        {
            audioSource.clip = heavyBreathClip;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
    }

    public void HandleStamina()
    {
        if (movement.isSprinting)
        {
            currentStamina -= drainRate * Time.deltaTime;
            if (currentStamina < 0f)
                currentStamina = 0f;
        }
        else
        {
            currentStamina += recoverRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }

        if (staminaSlider != null)
            staminaSlider.value = currentStamina;

        if (audioSource != null && heavyBreathClip != null)
        {
            if (currentStamina < maxStamina)
            {
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
            else
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
        }
    }
}
