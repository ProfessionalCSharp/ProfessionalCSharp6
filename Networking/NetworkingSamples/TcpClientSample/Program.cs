using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace TcpClientSample
{
    class Program
    {
        private const string host = "localhost";
        private const int port = 8000;

        static void Main()
        {
            SendAndReceive().Wait();
            ReadLine();
        }

        public static async Task SendAndReceive()
        {
            using (var client = new TcpClient())
            {
                await client.ConnectAsync(host, port);
                using (NetworkStream stream = client.GetStream())
                using (var writer = new StreamWriter(stream, Encoding.ASCII, 1024, leaveOpen: true))
                using (var reader = new StreamReader(stream, Encoding.ASCII, true, 1024, leaveOpen: true))
                {
                    writer.AutoFlush = true;
                    string line = string.Empty;
                    do
                    {
                        WriteLine("enter a string, bye to exit");
                        line = ReadLine();
                        await writer.WriteLineAsync(line);
                       
                        string result = await reader.ReadLineAsync();
                        WriteLine($"received {result} from server");
                    } while (line != "bye");

                    WriteLine("so long, and thanks for all the fish");
                }
            }            
        }
    }
}
