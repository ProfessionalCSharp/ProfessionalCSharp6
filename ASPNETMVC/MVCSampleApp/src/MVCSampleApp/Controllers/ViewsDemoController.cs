using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCSampleApp.Controllers
{
    public class ViewsDemoController : Controller
    {
        private EventsAndMenusContext _context;
        public ViewsDemoController(EventsAndMenusContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();


        public IActionResult PassingData()
        {
            ViewBag.MyData = "Hello from the controller";
            return View();
        }

        public IActionResult PassingAModel()
        {
            var menus = new List<Menu>
              {
                new Menu { Id=1, Text="Schweinsbraten mit Knödel und Sauerkraut",
                           Price=6.9, Category="Main" },
                new Menu { Id=2, Text="Erdäpfelgulasch mit Tofu und Gebäck",
                           Price=6.9, Category="Vegetarian" },
                new Menu { Id=3,
                           Text="Tiroler Bauerngröst'l mit Spiegelei und Krautsalat",
                           Price=6.9, Category="Main" }
              };
            return View(menus);
        }

        public IActionResult LayoutSample() => View();

        public IActionResult LayoutUsingSections() => View();


        public IActionResult UseAPartialView1() => View(_context);


        public ActionResult UseAPartialView2() => View();


        public ActionResult ShowEvents()
        {
            ViewBag.EventsTitle = "Live Events";
            return PartialView(_context.Events);
        }

        public IActionResult UseViewComponent() => View();

    }
}
