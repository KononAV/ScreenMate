using System;
using System.IO;
using System.Threading;
using AnalizerLogic;
using static PublicReadonly;

class Program
{
    private static readonly string AssetsPath = @"D:/Unity/proj/ScreenMate/Assets/Scripts";

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

        watcher.NotifyFilter = NotifyFilters.Size;

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
                Console.WriteLine("TYPE: " + result.Type);
                Console.WriteLine("FIELD: " + result.Fields);
                AttributeGenerateTypes.Types.BaseTemplateByAttributeBuild(
                    result.Type,
                    result.Fields,
                    PublicReadonly.folder,
                    PublicReadonly.fileInfo,
                    PublicReadonly.GetStringCode
                );
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Error: {exception.Message}");
        }
    }
}
