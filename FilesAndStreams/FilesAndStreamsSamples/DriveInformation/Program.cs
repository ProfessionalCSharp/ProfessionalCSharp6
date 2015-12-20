using System.IO;
using static System.Console;

namespace DriveInformation
{
    public class Program
    {
        public static void Main()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    WriteLine($"Drive name: {drive.Name}");
                    WriteLine($"Format: {drive.DriveFormat}");
                    WriteLine($"Type: {drive.DriveType}");
                    WriteLine($"Root directory: {drive.RootDirectory}");
                    WriteLine($"Volume label: {drive.VolumeLabel}");
                    WriteLine($"Free space: {drive.TotalFreeSpace}");
                    WriteLine($"Available space: {drive.AvailableFreeSpace}");
                    WriteLine($"Total size: {drive.TotalSize}");
                    
                    WriteLine();
                   
                }
            }
        }
    }
}
