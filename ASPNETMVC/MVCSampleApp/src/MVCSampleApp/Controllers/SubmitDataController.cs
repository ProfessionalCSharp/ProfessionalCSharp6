using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using System.Threading.Tasks;

namespace MVCSampleApp.Controllers
{
    public class SubmitDataController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult CreateMenu() => View();
        public IActionResult CreateMenu2() => View();
        public IActionResult CreateMenu3() => View();

        [HttpPost]
        public IActionResult CreateMenu(int id, string text, double price,
            string category)
        {
            var m = new Menu { Id = id, Text = text, Price = price };
            ViewBag.Info =
              $"menu created: {m.Text}, Price: {m.Price}, category: {m.Category}";
            return View("Index");
        }

        [HttpPost]
        public IActionResult CreateMenu2(Menu m)
        {
            
            ViewBag.Info = 
                $"menu created: {m.Text}, Price: {m.Price}, category: {m.Category}";
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenu3Result()
        {
            var m = new Menu();
            bool updated = await TryUpdateModelAsync<Menu>(m);
            if (updated)
            {
                ViewBag.Info = $"menu created: {m.Text}, Price: {m.Price}, category: {m.Category}";
                return View("Index");
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult CreateMenu4(Menu m)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Info =
                  $"menu created: {m.Text}, Price: {m.Price}, category: {m.Category}";
            }
            else
            {
                ViewBag.Info = "not valid";
            }
            return View("Index");
        }
    }
}
