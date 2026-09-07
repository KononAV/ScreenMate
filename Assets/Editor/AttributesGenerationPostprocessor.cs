#if UNITY_EDITOR

using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class GenerationFileWatcher
{
    private static FileSystemWatcher watcher;

    private const string GeneratedFolder = "TemporalFolder";

    static GenerationFileWatcher()
    {
        StartWatcher();
    }

    private static void StartWatcher()
    {
        if (watcher != null)
            return;

        watcher = new FileSystemWatcher(Application.dataPath, "*.cs");

        watcher.NotifyFilter =
            NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName;

        watcher.Changed += OnFileChanged;
        watcher.Created += OnFileChanged;
        watcher.Renamed += OnFileChanged;

        watcher.EnableRaisingEvents = true;

        Debug.Log("[Generator] File watcher started");
    }

    private static void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        string path = e.FullPath;

        // Не обрабатываем generated-файлы
        if (
            path.Contains(
                Path.DirectorySeparatorChar + GeneratedFolder + Path.DirectorySeparatorChar
            )
        )
        {
            return;
        }

        // Нас интересуют только C#
        if (!path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        Debug.Log($"[Generator] Changed: {path}");

        // FileSystemWatcher работает не в главном потоке Unity.
        // Поэтому передаём выполнение в Unity Editor.
        EditorApplication.delayCall += () =>
        {
            ProcessFile(path);
        };
    }

    private static void ProcessFile(string path)
    {
        if (!File.Exists(path))
            return;

        string assetPath = FileUtil.GetProjectRelativePath(path);

        if (string.IsNullOrEmpty(assetPath))
            return;

        Debug.Log($"[Generator] Processing: {assetPath}");

        // Просим Unity обновить AssetDatabase
        AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

        MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);

        if (script == null)
            return;

        Type type = script.GetClass();

        if (type == null)
            return;

        // Генерируем только классы с нужным атрибутом
        if (!Attribute.IsDefined(type, typeof(PublicReadonlyAttribute)))
        {
            return;
        }

        Debug.Log($"[Generator] Found [GeneratePartial]: {type.FullName}");

        FieldInfo[] fields = type.GetFields(
            BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.Public
                | BindingFlags.NonPublic
        );

        foreach (FieldInfo fieldInfo in fields)
        {
            if (!Attribute.IsDefined(fieldInfo, typeof(PublicReadonlyAttribute)))
            {
                continue;
            }

            Debug.Log($"[Generator] Generate: {fieldInfo.Name}");

            AGLogic.Generation.Generate(type, fieldInfo);
        }
    }
}

#endif
