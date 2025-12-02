using System.Collections;
using UnityEngine;
using TMPro;

public class Flashlight : MonoBehaviour
{
    [Header("FlashLight Object")]
    [SerializeField] private GameObject flashlightLight;

    [Header("Audio Flashlight")]
    public AudioSource audioSource;
    public AudioClip flashlightOnSound;
    public AudioClip flashlightOffSound;
    public AudioClip tapSound;

    [Header("Flashlight Random Off Settings")]
    public float minOnTime = 5f;
    public float maxOnTime = 12f;
    public float flickerDuration = 1.6f;

    [Header("UI Warning")]
    public TextMeshProUGUI warningText;

    [Header("Spam Settings")]
    public int minSpamCount = 5;
    public int maxSpamCount = 7;
    public float spamResetTime = 1.2f;

    public System.Action<bool> OnFlashlightStateChanged;

    private bool flashlightActive = false;
    private Coroutine randomOffRoutine;

    private int spamCount = 0;
    private float spamTimer = 0f;
    private int requiredSpam = 0;
    private bool needsSpam = false;

    void Start()
    {
        flashlightLight.SetActive(false);

        if (warningText != null)
            warningText.gameObject.SetActive(false);

        OnFlashlightStateChanged?.Invoke(false);
    }

    void Update()
    {
        if (needsSpam)
        {
            spamTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.F))
            {
                spamCount++;
                spamTimer = 0f;

                if (tapSound != null)
                    audioSource.PlayOneShot(tapSound);

                if (spamCount >= requiredSpam)
                {
                    RecoverFlashlight();
                }
            }

            if (spamTimer > spamResetTime)
            {
                spamCount = 0;
                spamTimer = 0f;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        flashlightActive = !flashlightActive;
        flashlightLight.SetActive(flashlightActive);

        OnFlashlightStateChanged?.Invoke(flashlightActive);

        audioSource.clip = flashlightActive ? flashlightOnSound : flashlightOffSound;
        audioSource.Play();

        if (flashlightActive && warningText != null)
            warningText.gameObject.SetActive(false);

        if (flashlightActive)
        {
            if (randomOffRoutine != null) StopCoroutine(randomOffRoutine);
            randomOffRoutine = StartCoroutine(RandomTurnOff());
        }
        else
        {
            if (randomOffRoutine != null) StopCoroutine(randomOffRoutine);
        }
    }

    IEnumerator RandomTurnOff()
    {
        yield return new WaitForSeconds(Random.Range(minOnTime, maxOnTime));

        float timer = 0f;
        while (timer < flickerDuration)
        {
            flashlightLight.SetActive(!flashlightLight.activeSelf);
            yield return new WaitForSeconds(Random.Range(0.12f, 0.25f));
            timer += Time.deltaTime;
        }

        flashlightActive = false;
        flashlightLight.SetActive(false);

        OnFlashlightStateChanged?.Invoke(false);

        needsSpam = true;
        spamCount = 0;
        spamTimer = 0f;
        requiredSpam = Random.Range(minSpamCount, maxSpamCount + 1);

        if (warningText != null)
        {
            warningText.text = $"ไฟฉายดับ! กด F รัวๆ";
            warningText.gameObject.SetActive(true);
        }
    }

    void RecoverFlashlight()
    {
        needsSpam = false;
        spamCount = 0;

        flashlightActive = true;
        flashlightLight.SetActive(true);

        OnFlashlightStateChanged?.Invoke(true);

        audioSource.clip = flashlightOnSound;
        audioSource.Play();

        if (warningText != null)
            warningText.gameObject.SetActive(false);

        if (randomOffRoutine != null) StopCoroutine(randomOffRoutine);
        randomOffRoutine = StartCoroutine(RandomTurnOff());
    }
}
