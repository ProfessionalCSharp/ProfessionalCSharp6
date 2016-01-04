using ClientApp.DemoService;
using System.ServiceModel;
using static System.Console;

namespace ClientApp
{
    public class Program
    {
        private class CallbackHandler : IDemoServiceCallback
        {
            public void SendMessage(string message)
            {
                WriteLine($"message from the server {message}");
            }
        }

        public static void Main()
        {
            WriteLine("client... wait for the server");
            ReadLine();
            StartSendRequest();
            WriteLine("next return to exit");
            ReadLine();
            WriteLine("bye...");
        }

        static async void StartSendRequest()
        {
            var callbackInstance = new InstanceContext(new CallbackHandler());
            var client = new DemoServiceClient(callbackInstance);
            await client.StartSendingMessagesAsync();
        }
    }
}
