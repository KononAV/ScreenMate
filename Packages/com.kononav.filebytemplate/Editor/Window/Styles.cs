using UnityEditor;
using UnityEngine;

public class WindowStyles
{
    public void GetAnalizingFolderField()
    {
        GUIStyle style = new GUIStyle(EditorStyles.label)
        {
            fontSize = 12,
            fontStyle = FontStyle.Bold,
        };

        EditorGUILayout.LabelField("Analizing folder: ", EditorData.WATCHER_ASSETS_PATH, style);
    }
}
