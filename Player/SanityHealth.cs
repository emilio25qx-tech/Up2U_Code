using UnityEngine;
using UnityEngine.UI;

public class SanityHealth : MonoBehaviour
{
    [Header("Sanity Settings")]
    public float maxSanity = 100f;
    public float sanity = 100f;
    public float sanityDecreaseRate = 5f;
    public float sanityRecoverRate = 3f;

    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float health = 100f;
    public float healthDecreaseRate = 5f;

    [Header("UI Sliders")]
    public Slider sanitySlider;
    public Slider healthSlider;

    [Header("Heartbeat Sound")]
    public AudioSource heartbeatSource;
    public AudioClip heartbeatClip;

    private bool flashlightOn = false;
    private bool gamePaused = false;

    public FadeOutUIDead fadeOutDead;

    public Player player;

    void Start()
    {
        Flashlight fl = FindFirstObjectByType<Flashlight>();
        fl.OnFlashlightStateChanged += HandleFlashlightChange;

        sanitySlider.maxValue = maxSanity;
        sanitySlider.value = sanity;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    void Update()
    {
        if (!gamePaused)
        {
            UpdateSanity();
        }
        UpdateUI();

        if (gamePaused && heartbeatSource != null && heartbeatSource.isPlaying)
        {
            heartbeatSource.Pause();
        }
    }

    void HandleFlashlightChange(bool isOn)
    {
        flashlightOn = isOn;
    }

    void UpdateSanity()
    {
        if (!flashlightOn)
            sanity -= sanityDecreaseRate * Time.deltaTime;
        else
            sanity += sanityRecoverRate * Time.deltaTime;

        sanity = Mathf.Clamp(sanity, 0, maxSanity);

        HandleHeartbeat();

        if (sanity <= 0)
            DecreaseHealth();
    }

    void HandleHeartbeat()
    {
        if (gamePaused) return;

        if (sanity < 20f)
        {
            if (!heartbeatSource.isPlaying)
            {
                heartbeatSource.clip = heartbeatClip;
                heartbeatSource.loop = true;
                heartbeatSource.Play();
            }
        }
        else
        {
            if (heartbeatSource.isPlaying)
                heartbeatSource.Stop();
        }
    }

    void DecreaseHealth()
    {
        if (health > 0)
        {
            health -= healthDecreaseRate * Time.deltaTime;
            health = Mathf.Clamp(health, 0, maxHealth);
        }

        if (health <= 0)
        {
            Debug.Log("Dead");
            MuteAllAudioExcept(fadeOutDead);

            if (fadeOutDead != null)
            {
                fadeOutDead.FadeOut("You Died...");
            }

            if (player != null)
            {
                player.enabled = false;
            }

            gamePaused = true;
        }
    }

    void MuteAllAudioExcept(FadeOutUIDead exclude)
    {
        AudioSource[] allAudio = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audio in allAudio)
        {
            if (exclude != null)
            {
                if (audio == exclude.musicSource || audio == exclude.deadSource)
                    continue;
            }
            audio.Stop();
        }
    }

    void UpdateUI()
    {
        sanitySlider.value = sanity;
        healthSlider.value = health;
    }

    public void SetGamePaused(bool paused)
    {
        gamePaused = paused;
    }
}
