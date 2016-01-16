using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinAppPackageSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var pm = new PackageManager();
            IEnumerable<Package> packages = pm.FindPackages();
            foreach (var package in packages)
            {
                try
                {
                    WriteLine($"Architecture: {package.Id.Architecture.ToString()}");
                    WriteLine($"Family: {package.Id.FamilyName}");
                    WriteLine($"Full name: {package.Id.FullName}",);
                    WriteLine($"Name: {package.Id.Name}");
                    WriteLine($"Publisher: {package.Id.Publisher}");
                    WriteLine($"Publisher Id: {package.Id.PublisherId}",);
                    if (package.InstalledLocation != null)
                        WriteLine(package.InstalledLocation.Path);
                    WriteLine();
                }
                catch (FileNotFoundException ex)
                {
                    WriteLine($"{ex.Message}, file: {ex.FileName}");
                }
            }
            ReadLine();

        }
    }
}
