using System;
using System.Collections.Generic;
using System.Linq;
// RC2 using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.WebEncoders;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCSampleApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index() => View();

        public string Hello() => "Hello, ASP.NET MVC 6";

        public string Greeting(string name) =>
            HtmlEncoder.Default.HtmlEncode($"Hello, {name}");
            // RC2            HtmlEncoder.Default.Encode($"Hello, {name}");

        public string Greeting2(string id) =>
            HtmlEncoder.Default.HtmlEncode($"Hello, {id}");
            // RC2            HtmlEncoder.Default.Encode($"Hello, {id}");





    }
}
