using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCSampleApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public string Hello() => "Hello, ASP.NET MVC 6";

        public string Greeting(string name) =>
            HtmlEncoder.Default.Encode($"Hello, {name}");

        public string Greeting2(string id) =>
            HtmlEncoder.Default.Encode($"Hello, {id}");

        public int Add(int x, int y) => x + y;

    }
}
