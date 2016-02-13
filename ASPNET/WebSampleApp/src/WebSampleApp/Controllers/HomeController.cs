using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSampleApp.Services;

namespace WebSampleApp.Controllers
{
    public class HomeController
    {
        private readonly ISampleService _service;
        public HomeController(ISampleService service)
        {
            _service = service;
        }

        public async Task<int> Index(HttpContext context)
        {
            var sb = new StringBuilder();
            sb.Append("<ul>");
            sb.Append(string.Join("", _service.GetSampleStrings().Select(
                s => $"<li>{s}</li>").ToArray()));
            sb.Append("</ul>");
            await context.Response.WriteAsync(sb.ToString());
            return 200;
        }
    }

}
