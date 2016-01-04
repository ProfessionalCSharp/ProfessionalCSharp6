using System.ServiceModel;
using System.Threading.Tasks;
using static System.Console;

namespace Wrox.ProCSharp.WCF
{
    class ClientCallback : IMyMessageCallback
    {
        public void OnCallback(string message)
        {
            WriteLine($"message from the server: {message}");
        }
    }


    public class Program
    {
        public static void Main()
        {
            WriteLine("Client - wait for service");
            ReadLine();

            DuplexSample();

            WriteLine("Client - press return to exit");
            ReadLine();

        }

        private async static void DuplexSample()
        {
            var binding = new WSDualHttpBinding();
            var address =
                  new EndpointAddress("http://localhost:8733/Design_Time_Addresses/MessageService/Service1/");

            var clientCallback = new ClientCallback();
            var context = new InstanceContext(clientCallback);

            var factory = new DuplexChannelFactory<IMyMessage>(context, binding, address);

            IMyMessage messageChannel = factory.CreateChannel();

            await Task.Run(() => messageChannel.MessageToServer("From the server"));
        }
    }
}
