using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace DataProtectionSample
{
    class Program
    {
        private const string readOption = "-r";
        private const string writeOption = "-w";
        private static readonly string[] options = { readOption, writeOption };

        static void Main(string[] args)
        {
            if (args.Length != 2 || args.Intersect(options).Count() != 1)
            {
                ShowUsage();
                return;
            }
            string fileName = args[1];

            MySafe safe = InitProtection();


            switch (args[0])
            {
                case writeOption:
                    Write(safe, fileName);
                    break;
                case readOption:
                    Read(safe, fileName);
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }

        public static MySafe InitProtection()
        {
            var serviceCollection = new ServiceCollection();   
            serviceCollection.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("."))
                .SetDefaultKeyLifetime(TimeSpan.FromDays(20))
                .ProtectKeysWithDpapi();
          
            IServiceProvider services = serviceCollection.BuildServiceProvider();

            return ActivatorUtilities.CreateInstance<MySafe>(services);
        }

        public static void Read(MySafe safe, string fileName)
        {
            string encrypted = File.ReadAllText(fileName);
            string decrypted = safe.Decrypt(encrypted);
            WriteLine(decrypted);
        }

        public static void Write(MySafe safe, string fileName)
        {
            WriteLine("enter content to write:");
            string content = ReadLine();
            string encrypted = safe.Encrypt(content);
            File.WriteAllText(fileName, encrypted);
            WriteLine($"content written to {fileName}");
        }

        private static void ShowUsage()
        {
            WriteLine("Usage: DataProtectionSample options filename");
            WriteLine("Options:");
            WriteLine("\t-r Read");
            WriteLine("\t-w Write");
            WriteLine();
        }
    }
}
