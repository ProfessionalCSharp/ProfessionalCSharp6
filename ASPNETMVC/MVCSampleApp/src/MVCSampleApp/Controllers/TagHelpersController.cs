using Microsoft.AspNet.Mvc;
using MVCSampleApp.Models;
using System;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCSampleApp.Controllers
{
    public class TagHelpersController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index() => View();

        public IActionResult LabelHelper() => View(GetSampleMenu());

        public IActionResult InputHelper() => View(GetSampleMenu());

        public IActionResult FormHelper() => View(GetSampleMenu());


        [HttpPost]
        public IActionResult FormHelper(Menu m)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            ViewBag.Status = "Success";
            return View("ValidationHelperResult", m);            
        }

        public IActionResult CustomHelper() => View(GetSampleMenus());

       

        private Menu GetSampleMenu() =>
            new Menu
            {
                Id = 1,
                Text = "Schweinsbraten mit Knödel und Sauerkraut",
                Price = 6.9,
                Date = new DateTime(2016, 10, 5),
                Category = "Main"
            };

        private IList<Menu> GetSampleMenus() =>
            new List<Menu>()
            {
                new Menu
                {
                    Id = 1,
                    Text = "Schweinsbraten mit Knödel und Sauerkraut",
                    Price = 8.5,
                    Date = new DateTime(2016, 10, 5),
                    Category = "Main"
                },
                new Menu
                {
                    Id = 2,
                    Text = "Erdäpfelgulasch mit Tofu und Gebäck",
                    Price = 8.5,
                    Date = new DateTime(2016, 10, 6),
                    Category = "Vegetarian"
                },
                new Menu
                {
                    Id = 3,
                    Text = "Tiroler Bauerngröst'l mit Spiegelei und Krautsalat",
                    Price = 8.5,
                    Date = new DateTime(2016, 10, 7),
                    Category = "Vegetarian"
                }
            };
    }
}
