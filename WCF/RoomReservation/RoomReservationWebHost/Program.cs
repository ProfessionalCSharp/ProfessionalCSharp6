using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Wrox.ProCSharp.WCF;
using static System.Console;

namespace RoomReservationWebHost
{
    class Program
    {
        static void Main()
        {
            var baseAddress = new Uri("http://localhost:8000/RoomReservation");
            var host = new WebServiceHost(typeof(RoomReservationService), baseAddress);
            host.Open();

            WriteLine("service running");
            WriteLine("Press return to exit...");
            ReadLine();

            if (host.State == CommunicationState.Opened)
                host.Close();

        }
    }
}
