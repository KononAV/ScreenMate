#if UNITY_EDITOR

using System;
using System.Reflection;
using UnityEditor;

public class GenerationPostprocessor : AssetPostprocessor
{
    private const string GeneratedFolder = "Assets/TemporalFolder/";

    private static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths
    )
    {
        foreach (string path in importedAssets)
        {
            // Только C# файлы
            if (!path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                continue;

            // Игнорируем сгенерированные файлы
            if (path.StartsWith(GeneratedFolder, StringComparison.OrdinalIgnoreCase))
                continue;

            ProcessScript(path);
        }
    }

    private static void ProcessScript(string path)
    {
        MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);

        if (script == null)
            return;

        Type type = script.GetClass();

        if (type == null)
            return;

        // Генерируем ТОЛЬКО если у класса есть специальный атрибут
        if (!Attribute.IsDefined(type, typeof(PublicPropertyAttribute)))
        {
            return;
        }

        FieldInfo[] fields = type.GetFields(
            BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.Public
                | BindingFlags.NonPublic
        );

        foreach (FieldInfo fieldInfo in fields)
        {
            if (!Attribute.IsDefined(fieldInfo, typeof(PublicPropertyAttribute)))
            {
                continue;
            }

            AGLogic.Generation.Generate(type, fieldInfo);
        }
    }
}

#endif
