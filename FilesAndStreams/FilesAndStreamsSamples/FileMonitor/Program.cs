using System.IO;
using static System.Console;

namespace FileMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WatchFiles("c:/test", "*.txt");
            ReadLine();
        }

        public static void WatchFiles(string path, string filter)
        {
            var watcher = new FileSystemWatcher(path, filter)
            {
                IncludeSubdirectories = true
            };
            watcher.Created += OnFileChanged;
            watcher.Changed += OnFileChanged;
            watcher.Deleted += OnFileChanged;
            watcher.Renamed += OnFileRenamed;
           
            watcher.EnableRaisingEvents = true;
            WriteLine("watching file changes...");
        }


        private static void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            WriteLine($"file {e.OldName} {e.ChangeType} to {e.Name}");
        }

        private static void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            WriteLine($"file {e.Name} {e.ChangeType}");
        }
    }
}
