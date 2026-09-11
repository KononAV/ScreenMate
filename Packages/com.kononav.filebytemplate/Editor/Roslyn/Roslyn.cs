using System;
using System.IO;
using System.Threading;
using AnalizerLogic;
using NALogic;
using UnityEngine;
using static PublicReadonly;

public struct RoslynFileManipulations
{
    public FileSystemEventHandler OnFileCreated;

    public FileSystemEventHandler OnFileChanged;
    public FileSystemEventHandler OnFileDeleted;
}

class RoslynAnalizer
{
    private static FileSystemWatcher _watcher;
    private static FileSystemWatcher _utilsWatcher;

    public static void Main()
    {
        EXCEPTIONS.IsWorkDirectoryExist(EditorData.WATCHER_ASSETS_PATH);

        NativeAttributesLogic.CreateDirectory(EditorData.ATTRIBUTES_FOLDER);

        Debug.Log("Generator started.");
        Debug.Log("Waiting for changes...");

        RoslynLogic.WatcherSetup(
            _utilsWatcher,
            EditorData.ATTRIBUTES_FOLDER,
            new RoslynFileManipulations
            {
                OnFileCreated = RoslynLogic.OnAttributeFileChanged,
                OnFileChanged = RoslynLogic.OnAttributeFileChanged,
                OnFileDeleted = RoslynLogic.OnAttributeFileChanged,
            }
        );

        RoslynLogic.WatcherSetup(
            _watcher,
            EditorData.WATCHER_ASSETS_PATH,
            new RoslynFileManipulations { OnFileChanged = RoslynLogic.OnFileChanged }
        );
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
