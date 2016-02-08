using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace UdpReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            int port;
            string groupAddress;
            if (!ParseCommandLine(args, out port, out groupAddress))
            {
                ShowUsage();
                return;
            }
            ReaderAsync(port, groupAddress).Wait();
            ReadLine();
        }

        private static void ShowUsage()
        {
            WriteLine("Usage: UdpReceiver -p port  [-g groupaddress]");
        }

        private static bool ParseCommandLine(string[] args, out int port, out string groupAddress)
        {
            port = 0;
            groupAddress = string.Empty;
            if (args.Length < 2 || args.Length > 5)
            {
                return false;
            }
            if (args.SingleOrDefault(a => a == "-p") == null)
            {
                WriteLine("-p required");
                return false;
            }

            // get port number
            string port1 = GetValueForKey(args, "-p");
            if (port1 == null || !int.TryParse(port1, out port))
            {
                return false;
            }


            // get optional group address
            groupAddress = GetValueForKey(args, "-g");
            return true;
        }

        private static string GetValueForKey(string[] args, string key)
        {
            int? nextIndex = args.Select((a, i) => new { Arg = a, Index = i }).SingleOrDefault(a => a.Arg == key)?.Index + 1;
            if (!nextIndex.HasValue)
            {
                return null;
            }
            return args[nextIndex.Value];
        }



        private static async Task ReaderAsync(int port, string groupAddress)
        {
            using (var client = new UdpClient(port))
            {
                if (groupAddress != null)
                {
                    client.JoinMulticastGroup(IPAddress.Parse(groupAddress));
                    WriteLine($"joining the multicast group {IPAddress.Parse(groupAddress)}");
                }

                bool completed = false;
                do
                {
                    WriteLine("starting the receiver");
                    UdpReceiveResult result = await client.ReceiveAsync();
                    byte[] datagram = result.Buffer;
                    string received = Encoding.UTF8.GetString(datagram);
                    WriteLine($"received {received}");
                    if (received == "bye")
                    {
                        completed = true;
                    }
                } while (!completed);
                WriteLine("receiver closing");

                if (groupAddress != null)
                {
                    client.DropMulticastGroup(IPAddress.Parse(groupAddress));
                }
            }
        }


    }
}
