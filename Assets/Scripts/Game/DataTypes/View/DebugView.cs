using TMPro;
using UnityEngine;

[System.Serializable]
public struct DebugWindowSettings
{
    public float minWidth;
    public float minHeight;
    public float maxWidth;
    public float maxHeight;

    public void Setup(float minWidth, float minHeight, float maxWidth, float maxHeight)
    {
        this.minWidth = minWidth;
        this.minHeight = minHeight;
        this.maxWidth = maxWidth;
        this.maxHeight = maxHeight;
    }
}

[System.Serializable]
public class DebugView
{
    // public TMP_Text consoleText;
    // public RectTransform window;
    public DebugWindowSettings windowSettings = new DebugWindowSettings();
}
