using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDotnetFramework.Startup))]
namespace WebDotnetFramework
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
