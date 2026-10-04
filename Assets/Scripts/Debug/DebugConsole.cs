using System.Reflection.Metadata;
using TMPro;
using UnityEngine;

public class DebugConsole : MonoBehaviour
{
    [SerializeField]
    private TMP_Text consoleText;

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        consoleText.text += logString + "\n";
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
}
