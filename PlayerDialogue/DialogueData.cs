using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;
    [TextArea] public string message;
}

[System.Serializable]
public class DialogueData
{
    [Header("Dialogue List")]
    public DialogueLine[] lines;
}
