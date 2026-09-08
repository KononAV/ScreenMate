using System;
using System.IO;

string folder = @"D:\Unity\proj\ScreenMate\Assets\Scripts\Game";

using FileSystemWatcher watcher = new FileSystemWatcher();

watcher.Path = folder;
watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

watcher.Filter = "*.*";
watcher.IncludeSubdirectories = true;

watcher.Changed += OnFileChanged;

watcher.EnableRaisingEvents = true;

Console.WriteLine($"Отслеживаем: {folder}");
Console.WriteLine("Сохраняй файлы в VS Code...");
Console.WriteLine("Нажми Enter для выхода.");

Console.ReadLine();

void OnFileChanged(object sender, FileSystemEventArgs e)
{
    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Файл изменён: {e.Name}");
}
