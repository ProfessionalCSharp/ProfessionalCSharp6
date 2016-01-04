using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace WebSocketsSample
{
    public class DemoService : IDemoService
    {

        public void StartSendingMessages()
        {
            IDemoCallback callback = OperationContext.Current.GetCallbackChannel<IDemoCallback>();
            Task.Run(() => BackgroundSender(callback));
        }

        private async Task BackgroundSender(IDemoCallback callback)
        {
            int loop = 0;
            while ((callback as IChannel).State == CommunicationState.Opened)
            {
                await callback.SendMessage($"Hello from the server {loop++}");
                await Task.Delay(1000);
            }
        }
    }
}
