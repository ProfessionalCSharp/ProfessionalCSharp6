using System.ServiceModel;
using System.Threading.Tasks;
using static System.Console;

namespace Wrox.ProCSharp.WCF
{
    public class MessageService : IMyMessage
    {
        public void MessageToServer(string message)
        {
            WriteLine($"message from the client: {message}");
            IMyMessageCallback callback =
                  OperationContext.Current.
                        GetCallbackChannel<IMyMessageCallback>();

            callback.OnCallback("message from the server");

            Task.Run(() => TaskCallback(callback));
        }
        private async void TaskCallback(object callback)
        {
            IMyMessageCallback messageCallback = callback as IMyMessageCallback;
            for (int i = 0; i < 10; i++)
            {
                messageCallback.OnCallback($"message {i}");
                await Task.Delay(1000);
            }
        }
    }

}
