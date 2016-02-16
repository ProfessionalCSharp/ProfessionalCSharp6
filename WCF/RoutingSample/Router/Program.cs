using System;
using System.ServiceModel;
using System.ServiceModel.Routing;
using static System.Console;

namespace Router
{
    public class Program
    {
        internal static ServiceHost s_routerHost = null;

        public static void Main()
        {
            StartService();

            WriteLine("Router is running. Press return to exit");
            ReadLine();

            StopService();
        }

        internal static void StartService()
        {
            try
            {
                s_routerHost = new ServiceHost(typeof(RoutingService));
                s_routerHost.Faulted += myServiceHost_Faulted;
                s_routerHost.Open();

            }
            catch (AddressAccessDeniedException)
            {
                WriteLine("either start Visual Studio in elevated admin " +
                  "mode or register the listener port with netsh.exe");
            }
        }

        static void myServiceHost_Faulted(object sender, EventArgs e)
        {
            WriteLine("router faulted");
        }

        internal static void StopService()
        {
            if (s_routerHost != null && s_routerHost.State == CommunicationState.Opened)
            {
                s_routerHost.Close();
            }
        }
    }
}
