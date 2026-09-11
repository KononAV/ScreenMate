using System;
using System.IO;
using System.Threading;
using AnalizerLogic;
using NALogic;
using UnityEngine;

public static class RoslynLogic
{
    public static void WatcherSetup(
        FileSystemWatcher watcher,
        string watcherPath,
        RoslynFileManipulations manipulations
    )
    {
        watcher = new FileSystemWatcher(watcherPath, "*.cs");

        watcher.IncludeSubdirectories = true;
        watcher.NotifyFilter = NotifyFilters.Size;
        watcher.Created += manipulations.OnFileCreated;
        watcher.Changed += manipulations.OnFileChanged;
        watcher.Deleted += manipulations.OnFileDeleted;
        watcher.EnableRaisingEvents = true;
    }

    public static void OnAttributeFileChanged(object sender, FileSystemEventArgs e)
    {
        //NativeAttributesLogic.CreateDirectory(EditorData.UTILS_WATCHER_PATH);

        string sourcePath = EditorData.ATTRIBUTES_FOLDER;
        string destinationPath = EditorData.UTILS_WATCHER_PATH;

        NativeAttributesLogic.DeleteFolder(destinationPath);
        NativeAttributesLogic.CopyFolder(sourcePath, destinationPath);
    }

    public static void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        try
        {
            EXCEPTIONS.RecursionException(e);

            Thread.Sleep(100);

            var results = RoslynAnalyzer.Analyze(e.FullPath);

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
