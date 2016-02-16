using System.ServiceModel;
using Wrox.ProCSharp.WCF;
using static System.Console;

namespace HostOne
{
    public class Program
    {
        internal static ServiceHost myServiceHost = null;

        public static void Main()
        {
            string host = "Host One";
            DemoService.Server = host;

            StartService();

            WriteLine($"{host} is running. Press return to exit");
            ReadLine();

            StopService();
        }

        internal static void StartService()
        {
            try
            {
                myServiceHost = new ServiceHost(typeof(DemoService));
                myServiceHost.Open();
            }
            catch (AddressAccessDeniedException)
            {
                WriteLine("either start Visual Studio in elevated admin " +
                  "mode or register the listener port with netsh.exe");
            }
        }

        internal static void StopService()
        {
            if (myServiceHost != null && myServiceHost.State == CommunicationState.Opened)
            {
                myServiceHost.Close();
            }
        }
    }
}
