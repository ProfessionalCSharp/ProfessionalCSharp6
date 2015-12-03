using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaasWebHooksReceiverSample.Startup))]
namespace SaasWebHooksReceiverSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
