using System;
using System.IO;
using System.Threading;
using AnalizerLogic;
using UnityEngine;
using static PublicReadonly;

class RoslynAnalizer
{
    private static FileSystemWatcher watcher;

    public static void Main()
    {
        EXCEPTIONS.IsWorkDirectoryExist(EditorData.WATCHER_ASSETS_PATH);

        Debug.Log("Generator started.");
        Debug.Log("Waiting for changes...");

        WatcherSetup(EditorData.WATCHER_ASSETS_PATH);
    }

    private static void WatcherSetup(string AssetsPath)
    {
        watcher = new FileSystemWatcher(AssetsPath, "*.cs");

        watcher.IncludeSubdirectories = true;
        watcher.NotifyFilter = NotifyFilters.Size;
        watcher.Changed += OnFileChanged;
        watcher.EnableRaisingEvents = true;
    }

    private static void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        try
        {
            Debug.Log("CHANGED " + e.FullPath);

            Debug.Log("BEFORE EXCEPTION CHECK");

            EXCEPTIONS.RecursionException(e);

            Debug.Log("AFTER EXCEPTION CHECK");

            Thread.Sleep(100);

            Debug.Log("AFTER SLEEP");

            Debug.Log("BEFORE ANALYZE");

            Debug.Log("FILE: " + e.FullPath);
            Debug.Log("EXISTS: " + File.Exists(e.FullPath));
            Debug.Log("CONTENT:\n" + File.ReadAllText(e.FullPath));

            var results = RoslynAnalyzer.Analyze(e.FullPath);

            Debug.Log("AFTER ANALYZE");
            Debug.Log("RESULTS COUNT: " + results.Count);

            foreach (var result in results)
            {
                Debug.Log(result + " RESULT");

                Debug.Log("TYPE: " + result.Type);
                Debug.Log("FIELD: " + result.Fields);

                AttributeGenerateTypes.Types.BaseTemplateByAttributeBuild(
                    result.Type,
                    result.Fields,
                    PublicReadonly.Settings.Folder,
                    PublicReadonly.Settings.FileInfo,
                    PublicReadonly.GetStringCode
                );
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"Error: {exception}");
        }
    }
}

static class EXCEPTIONS
{
    public static void RecursionException(FileSystemEventArgs e)
    {
        if (e.FullPath.EndsWith(".PublicReadonly.cs", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("RECRUSION ATTRIBUTE EXCEPTION " + e);
        }
    }

    public static void IsWorkDirectoryExist(string assetsPath)
    {
        if (!Directory.Exists(assetsPath))
        {
            throw new Exception("WORKING DIRECTORY DOES NOT EXIST " + assetsPath);
        }
    }
}
