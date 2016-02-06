using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using static System.Console;

namespace CompressFileSample
{
    class Program
    {
        static void Main()
        {
            CompressFile("./test.txt", "./test.compressed");
            DecompressFile("./test.txt.gzip");

            CreateZipFile("c:/test", "c:/test2/test.zip");
        }

        public static void CreateZipFile(string directory, string zipFile)
        {
            FileStream zipStream = File.OpenWrite(zipFile);
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                IEnumerable<string> files = Directory.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(file));
                    using (FileStream inputStream = File.OpenRead(file))
                    using (Stream outputStream = entry.Open())
                    {
                        inputStream.CopyTo(outputStream);
                    }
                }
            }
        }

        public static void DecompressFile(string fileName)
        {
            FileStream inputStream = File.OpenRead(fileName);
            using (MemoryStream outputStream = new MemoryStream())
            using (var compressStream = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                compressStream.CopyTo(outputStream);
                outputStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(outputStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 4096, leaveOpen: true))
                {
                    string result = reader.ReadToEnd();
                    WriteLine(result);
                }
            }
        }

        public static void CompressFile(string fileName, string compressedFileName)
        {
            using (FileStream inputStream = File.OpenRead(fileName))
            {
                FileStream outputStream = File.OpenWrite(compressedFileName);
                using (var compressStream = new DeflateStream(outputStream, CompressionMode.Compress))
                {
                    inputStream.CopyTo(compressStream);
                }
            }

        }
    }
}
