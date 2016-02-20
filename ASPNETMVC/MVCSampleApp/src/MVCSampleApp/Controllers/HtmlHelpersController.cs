using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Extensions;
using MVCSampleApp.Models;
using System;
using System.Collections.Generic;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCSampleApp.Controllers
{
    public class HtmlHelpersController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index() => View();


        public IActionResult SimpleHelper() => View();

        public IActionResult HelperWithMenu() => View(GetSampleMenu());

        public IActionResult HelperList()
        {
            var cars = new Dictionary<int, string>();
            cars.Add(1, "Red Bull Racing");
            cars.Add(2, "McLaren");
            cars.Add(3, "Mercedes");
            cars.Add(4, "Ferrari");
            return View(cars.ToSelectListItems(4));
        }

        public IActionResult StronglyTypedMenu() => View(GetSampleMenu());

        public IActionResult MenuEditor() => View(GetSampleMenu());

        private Menu GetSampleMenu() =>
            new Menu
            {
                Id = 1,
                Text = "Schweinsbraten mit Knödel und Sauerkraut",
                Price = 6.9,
                Date = new DateTime(2016, 10, 5),
                Category = "Main"
            };
    }
}
