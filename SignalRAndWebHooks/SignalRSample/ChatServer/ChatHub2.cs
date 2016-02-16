using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace ChatServer
{
    public interface ISimpleClient
    {
        void BroadcastMessage(string name, string message);
    }

    public class ChatHub2 : Hub<ISimpleClient>
    {
        public void Send(string name, string message)
        {
            Clients.All.BroadcastMessage(name, message);
        }

        public override Task OnConnected()
        {
            // you can access info using base.Context
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
    }
}
