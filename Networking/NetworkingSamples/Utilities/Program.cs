using System;
using System.Net;
using static System.Console;

namespace Utilities
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                ShowUsage();
                return;
            }

            switch (args[0].ToLower())
            {
                case "uri":
                    UriSample(args[1]);
                    break;
                case "ipaddress":
                    IPAddressSample(args[1]);
                    break;
                default:
                    break;
            }
        }
        private static void ShowUsage()
        {
            WriteLine("Utilities command\n");
            WriteLine("commands:");
            WriteLine("\turi hostname\tShow the part of a URI, e.g uri www.wrox.com");
            WriteLine("\tipaddress ipaddress\tShow the part of a URI, e.g ipaddress 234.56.78.9");
            WriteLine("\t");

        }

        public static void IPAddressSample(string ipAddressString)
        {           
            IPAddress address;
            if (!IPAddress.TryParse(ipAddressString, out address))
            {
                WriteLine($"cannot parse {ipAddressString}");
                return;
            }

            byte[] bytes = address.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                WriteLine($"byte {i}: {bytes[i]:X}");
            }

            WriteLine($"family: {address.AddressFamily}, map to ipv6: {address.MapToIPv6()}, map to ipv4: {address.MapToIPv4()}");
            WriteLine($"IPv4 loopback address: {IPAddress.Loopback}");
            WriteLine($"IPv6 loopback address: {IPAddress.IPv6Loopback}");
            WriteLine($"IPv4 broadcast address: {IPAddress.Broadcast}");
            WriteLine($"IPv4 any address: {IPAddress.Any}");
            WriteLine($"IPv6 any address: {IPAddress.IPv6Any}");
        }

        public static void UriSample(string url)
        {
            var page = new Uri(url);
            WriteLine($"scheme: {page.Scheme}");
#if NET46
            WriteLine($"host: {page.Host}, type:  {page.HostNameType} ");

#else
            WriteLine($"host: {page.Host}, type:  {page.HostNameType}, idn host: {page.IdnHost}");
#endif
            WriteLine($"port: {page.Port}");
            WriteLine($"path: {page.AbsolutePath}");
            WriteLine($"query: {page.Query}");
            foreach (var segment in page.Segments)
            {
                WriteLine($"segment: {segment}");
            }

            var builder = new UriBuilder();
            builder.Host = "www.cninnovation.com";
            builder.Port = 80;
            builder.Path = "training/MVC";
            Uri uri = builder.Uri;
            WriteLine(uri);
        }
    }
}
