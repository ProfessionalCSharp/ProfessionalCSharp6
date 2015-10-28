using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSampleApp.Services;
using System.Text;
using Microsoft.AspNet.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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
            sb.Append(string.Join("", _service.GetSampleStrings().Select(s => $"<li>{s}</li>").ToArray()));
            sb.Append("</ul>");
            await context.Response.WriteAsync(sb.ToString());
            return 200;
        }
    }
}
