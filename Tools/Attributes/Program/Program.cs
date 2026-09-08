// using System;
// using System.IO;

// string folder = @"D:\Unity\proj\ScreenMate\Assets\Scripts\Game";

// using FileSystemWatcher watcher = new FileSystemWatcher();

// watcher.Path = folder;
// watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

// watcher.Filter = "*.*";
// watcher.IncludeSubdirectories = true;

// watcher.Changed += OnFileChanged;

// watcher.EnableRaisingEvents = true;

// Console.WriteLine($"Отслеживаем: {folder}");
// Console.WriteLine("Сохраняй файлы в VS Code...");
// Console.WriteLine("Нажми Enter для выхода.");

// Console.ReadLine();

// void OnFileChanged(object sender, FileSystemEventArgs e)
// {
//     Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Файл изменён: {e.Name}");
// }
using System;
using System.IO;
using System.Threading;
using AGLogic;
using AnalizerLogic;

class Program
{
    private static readonly string AssetsPath = @"D:/Unity/proj/ScreenMate/Assets";

    static void Main()
    {
        if (!Directory.Exists(AssetsPath))
        {
            Console.WriteLine($"Assets directory not found: {AssetsPath}");
            return;
        }

        Console.WriteLine("Generator started.");
        Console.WriteLine("Waiting for changes...");

        using var watcher = new FileSystemWatcher(AssetsPath, "*.cs");

        watcher.IncludeSubdirectories = true;

        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;

        watcher.Changed += OnFileChanged;

        watcher.EnableRaisingEvents = true;

        Console.ReadLine();
    }

    private static void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        if (e.FullPath.EndsWith(".Generate.cs", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        Thread.Sleep(100);

        try
        {
            var results = RoslynAnalyzer.Analyze(e.FullPath);

            foreach (var result in results)
            {
                Generation.Generate(result.Type, result.Fields);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error: {exception.Message}");
        }
    }
}
