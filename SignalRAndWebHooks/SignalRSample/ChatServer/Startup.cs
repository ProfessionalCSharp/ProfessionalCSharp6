using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ChatServer.Startup))]

namespace ChatServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
