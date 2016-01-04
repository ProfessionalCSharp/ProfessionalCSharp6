using System.ServiceModel;
using static System.Console;

namespace Wrox.ProCSharp.WCF
{
    public class Program
    {
        internal static ServiceHost myServiceHost = null;

        internal static void StartService()
        {
            //Instantiate new ServiceHost 
            myServiceHost = new ServiceHost(typeof(Wrox.ProCSharp.WCF.MessageService));

            //Open myServiceHost
            myServiceHost.Open();
        }

        internal static void StopService()
        {
            //Call StopService from your shutdown logic (i.e. dispose method)
            if (myServiceHost.State != CommunicationState.Closed)
                myServiceHost.Close();
        }

        public static void Main()
        {
            StartService();
            WriteLine("Service running; press return to exit");
            ReadLine();
            StopService();
            WriteLine("stopped");
        }
    }
}
