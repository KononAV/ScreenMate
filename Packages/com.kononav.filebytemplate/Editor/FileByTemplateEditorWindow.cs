using UnityEditor;
using UnityEngine;

namespace Alisakonon32.FileByTemplate.Editor
{
    public class PublicReadonlyWindow : EditorWindow
    {
        private string folder = "Assets/Generated";
        private bool autoGenerate = true;
        private bool generateOnSave = true;

        [MenuItem("Tools/Public Readonly Generator")]
        public static void ShowWindow()
        {
            GetWindow<PublicReadonlyWindow>("Public Readonly");
        }

        private void OnGUI()
        {
            GUILayout.Label("Public Readonly Generator", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            folder = EditorGUILayout.TextField("Output Folder", folder);

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
            EditorUtility.RevealInFinder(System.IO.Path.GetFullPath(folder));
        }
    }
}
