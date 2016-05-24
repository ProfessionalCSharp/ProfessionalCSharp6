using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCSampleApp.Controllers
{
    public class ResultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContentDemo() =>
            Content("Hello World", "text/plain");

        public IActionResult JsonDemo()
        {
            var m = new Menu
            {
                Id = 3,
                Text = "Grilled sausage with sauerkraut and potatoes",
                Price = 12.90,
                Date = new DateTime(2016, 3, 31),
                Category = "Main"
            };
            return Json(m);
        }

        public IActionResult RedirectDemo() => Redirect("http://www.cninnovation.com");



        public IActionResult RedirectRouteDemo() =>
            RedirectToRoute(new { controller = "Home", action = "Hello" });

        public IActionResult FileDemo() =>
            File("~/Images/Matthias.jpg", "image/jpeg");


    }
}
