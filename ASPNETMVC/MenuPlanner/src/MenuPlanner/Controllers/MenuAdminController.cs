using MenuPlanner.Models;
using MenuPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuPlanner.Controllers
{
    public class MenuAdminController : Controller
    {
        private readonly IMenuCardsService _service;
        public MenuAdminController(IMenuCardsService service)
        {
            _service = service;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View((await _service.GetMenusAsync()).OrderBy(m => m.Order));
        }

        public async Task<IActionResult> Details(int? id = 0)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Menu menu = await _service.GetMenuByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCardId = new SelectList(cards, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id","MenuCardId", "Text", "Price", "Active", "Order", "Type", "Day")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _service.AddMenuAsync(menu);
                return RedirectToAction("Index");
            }

            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCards = new SelectList(cards, "Id", "Name");
            return View(menu);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Menu menu = await _service.GetMenuByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }

            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCards = new SelectList(cards, "Id", "Name", menu.MenuCardId);
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id", "MenuCardId", "Text", "Price", "Order", "Type", "Day")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateMenuAsync(menu);
                return RedirectToAction("Index");
            }

            IEnumerable<MenuCard> cards = await _service.GetMenuCardsAsync();
            ViewBag.MenuCards = new SelectList(cards, "Id", "Name", menu.MenuCardId);
            return View(menu);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Menu menu = await _service.GetMenuByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Menu menu = await _service.GetMenuByIdAsync(id);
            await _service.DeleteMenuAsync(menu.Id);
            return RedirectToAction("Index");
        }

    }
}