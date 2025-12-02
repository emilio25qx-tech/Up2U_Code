using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HintTrigger : MonoBehaviour
{
    [Header("Hint Settings")]
    [Tooltip("เวลาที่ผู้เล่นอยู่ในห้องจนแสดง Hint (วินาที)")]
    public float hintDelay = 30f;

    [Header("Hint Messages")]
    public List<HintLineData> hintLines = new List<HintLineData>();

    [Header("Options")]
    public bool playOnce = true;

    private bool hintTriggered = false;
    private bool playerInside = false;
    private float timer = 0f;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        playerInside = false;
        timer = 0f; // ออกห้อง → รีเซ็ตเวลา
    }

    private void Update()
    {
        if (playerInside && !hintTriggered)
        {
            timer += Time.deltaTime;

            if (timer >= hintDelay)
            {
                ShowRandomHint();
                hintTriggered = true;
            }
        }
    }

    private void ShowRandomHint()
    {
        if (hintLines.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, hintLines.Count);
        HintLineData selectedHint = hintLines[randomIndex];

        // ส่ง hint ไป HintManager
        HintManager.Instance.ShowHint(selectedHint);
    }
}
