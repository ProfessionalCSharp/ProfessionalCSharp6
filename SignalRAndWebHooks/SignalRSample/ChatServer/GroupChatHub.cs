using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Linq;

namespace ChatServer
{
    public interface IGroupClient
    {
        void MessageToGroup(string groupName, string name, string message);
    }
    
    public class GroupChatHub : Hub<IGroupClient>
    {
        public Task AddGroup(string groupName) =>
            Groups.Add(Context.ConnectionId, groupName);           


        public Task LeaveGroup(string groupName) =>
            Groups.Remove(Context.ConnectionId, groupName);


        public void Send(string group, string name, string message)
        {
            Clients.Group(group).MessageToGroup(group, name, message);
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
