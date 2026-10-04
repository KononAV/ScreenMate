using TMPro;
using UnityEngine;

namespace DebugLogic
{
    static class DEBUG
    {
        public static void HandleLog(
            TMP_Text consoleText,
            string logString,
            string stackTrace,
            LogType type
        )
        {
            consoleText.text += logString + "\n";
        }

        public static void Subscribe(TMP_Text consoleText)
        {
            Application.logMessageReceived += (logString, stackTrace, type) =>
                HandleLog(consoleText, logString, stackTrace, type);
        }

        public static void UnSubscribe(TMP_Text consoleText)
        {
            Application.logMessageReceived -= (logString, stackTrace, type) =>
                HandleLog(consoleText, logString, stackTrace, type);
        }
    }
}
