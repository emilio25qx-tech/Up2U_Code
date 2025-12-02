using UnityEngine;

[System.Serializable]
public class HintLineData
{
    [Tooltip("ชื่อคนพูด")]
    public string speakerName = "???";

    [TextArea]
    [Tooltip("ข้อความ Hint")]
    public string message;
}
