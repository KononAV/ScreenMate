using System;
using System.IO;
using System.Threading;
using AnalizerLogic;
using static PublicReadonly;

class Program
{
    private static readonly string AssetsPath = @"D:/Unity/proj/ScreenMate/Assets/Scripts";
    private static readonly string AttributesPath =
        @"D:\Unity\proj\ScreenMate\Assets\Scripts\Utils\Attibutes";

    static void Main()
    {
        EXCEPTIONS.IsWorkDirectoryExist(AssetsPath);

        Console.WriteLine("Generator started.");
        Console.WriteLine("Waiting for changes...");

        WatcherSetup(AssetsPath);

        Console.ReadLine();
    }

    private static WatcherSetup(string AssetsPath)
    {
        using var watcher = new FileSystemWatcher(AssetsPath, "*.cs");

        watcher.IncludeSubdirectories = true;

        watcher.NotifyFilter = NotifyFilters.Size;

        watcher.Changed += OnFileChanged;

        watcher.EnableRaisingEvents = true;
    }

    private static void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        EXCEPTIONS.RecursionException(e);
        Thread.Sleep(100);

        try
        {
            var results = RoslynAnalyzer.Analyze(e.FullPath); //! change

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

static class EXCEPTIONS
{
    public static void RecursionException(FileSystemEventArgs e)
    {
        if (e.FullPath.EndsWith(".Generate.cs", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("RECRUSION ATTRIBUTE EXCEPTION " + e.ToString());
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
