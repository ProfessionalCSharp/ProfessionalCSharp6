using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                ShowUsage();
                return;
            }
            string hostName = args[0];
            int port;
            if (!int.TryParse(args[1], out port))
            {
                ShowUsage();
                return;
            }
            WriteLine("press return when the server is started");
            ReadLine();

            SendAndReceive(hostName, port).Wait();
            ReadLine();
        }

        private static void ShowUsage()
        {
            WriteLine("Usage: SocketClient server port");
        }

        public static async Task SendAndReceive(string hostName, int port)
        {
            try
            {
                IPHostEntry ipHost = await Dns.GetHostEntryAsync(hostName);
                IPAddress ipAddress = ipHost.AddressList.Where(address => address.AddressFamily == AddressFamily.InterNetwork).First();
                if (ipAddress == null)
                {
                    WriteLine("no IPv4 address");
                    return;
                }

                using (var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    client.Connect(ipAddress, port);
                    WriteLine("client successfully connected");
                    var stream = new NetworkStream(client);
                    var cts = new CancellationTokenSource();

                    Task tSender = Sender(stream, cts);
                    Task tReceiver = Receiver(stream, cts.Token);
                    await Task.WhenAll(tSender, tReceiver);

                }
            }
            catch (SocketException ex)
            {
                WriteLine(ex.Message);
            }
        }

        public static async Task Sender(NetworkStream stream, CancellationTokenSource cts)
        {
            WriteLine("Sender task");
            while (true)
            {
                WriteLine("enter a string to send, shutdown to exit");
                string line = ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes($"{line}\r\n");
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();
                if (string.Compare(line, "shutdown", ignoreCase: true) == 0)
                {
                    cts.Cancel();
                    WriteLine("sender task closes");
                    break;
                }
            }
        }

        private const int ReadBufferSize = 1024;

        public static async Task Receiver(NetworkStream stream, CancellationToken token)
        {
            try
            {
                stream.ReadTimeout = 5000;
                WriteLine("Receiver task");
                byte[] readBuffer = new byte[ReadBufferSize];
                while (true)
                {
                    Array.Clear(readBuffer, 0, ReadBufferSize);

                    int read = await stream.ReadAsync(readBuffer, 0, ReadBufferSize, token);
                    string receivedLine = Encoding.UTF8.GetString(readBuffer, 0, read);
                    WriteLine($"received {receivedLine}");
                }
            }
            catch (OperationCanceledException ex)
            {
                WriteLine(ex.Message);
            }
        }
    }
}
