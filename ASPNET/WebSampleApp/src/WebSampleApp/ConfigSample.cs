using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace WebSampleApp
{
    public static class ConfigSample
    {
        public static async Task AppSettings(HttpContext context, IConfigurationRoot config)
        {
            string settings = config["AppSettings:SiteName"];
            await context.Response.WriteAsync(settings.Div());
        }

        public static async Task ReadDatabaseConnection(HttpContext context, IConfigurationRoot config)
        {
            string connectionString = config["Data:DefaultConnection:ConnectionString"];
            await context.Response.WriteAsync(connectionString.Div());
        }

        public static async Task UserSecret(HttpContext context, IConfigurationRoot config)
        {
            string secret = config["UserSecret1"];
            await context.Response.WriteAsync(secret.Div());
        }
    }
}
