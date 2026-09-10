using UnityEditor;
using UnityEngine;

namespace KononAV.FileByTemplate.Editor
{
    public class PublicReadonlyWindow : EditorWindow
    {
        private bool autoGenerate = true;
        private bool generateOnSave = true;

        [MenuItem("Tools/Public Readonly Generator")]
        public static void ShowWindow()
        {
            GetWindow<PublicReadonlyWindow>("Public Readonly");
        }

        private void OnGUI()
        {
            WindowStyles style = new();

            GUILayout.Label("Public Readonly Generator", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            style.GetAnalizingFolderField();

            EditorGUILayout.Space();

            autoGenerate = EditorGUILayout.Toggle("Auto Generate", autoGenerate);

            generateOnSave = EditorGUILayout.Toggle("Generate On Save", generateOnSave);

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate"))
            {
                //Generate();
            }

            if (GUILayout.Button("Open Generated Folder"))
            {
                //OpenFolder();
            }
        }

        private void Generate()
        {
            Debug.Log("Generation started");

            // Здесь позже вызовешь свой генератор.
        }

        private void OpenFolder()
        {
            //EditorUtility.RevealInFinder(System.IO.Path.GetFullPath(folder));
        }
    }
}
