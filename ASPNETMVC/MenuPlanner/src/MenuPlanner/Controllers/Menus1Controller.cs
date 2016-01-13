using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MenuPlanner.Models;

namespace MenuPlanner.Controllers
{
    public class Menus1Controller : Controller
    {
        private MenuCardsContext _context;

        public Menus1Controller(MenuCardsContext context)
        {
            _context = context;    
        }

        // GET: Menus1
        public async Task<IActionResult> Index()
        {
            var menuCardsContext = _context.Menus.Include(m => m.MenuCard);
            return View(await menuCardsContext.ToListAsync());
        }

        // GET: Menus1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Menu menu = await _context.Menus.SingleAsync(m => m.Id == id);
            if (menu == null)
            {
                return HttpNotFound();
            }

            return View(menu);
        }

        // GET: Menus1/Create
        public IActionResult Create()
        {
            ViewData["MenuCardId"] = new SelectList(_context.MenuCards, "Id", "MenuCard");
            return View();
        }

        // POST: Menus1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MenuCardId"] = new SelectList(_context.MenuCards, "Id", "MenuCard", menu.MenuCardId);
            return View(menu);
        }

        // GET: Menus1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Menu menu = await _context.Menus.SingleAsync(m => m.Id == id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewData["MenuCardId"] = new SelectList(_context.MenuCards, "Id", "MenuCard", menu.MenuCardId);
            return View(menu);
        }

        // POST: Menus1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Update(menu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MenuCardId"] = new SelectList(_context.MenuCards, "Id", "MenuCard", menu.MenuCardId);
            return View(menu);
        }

        // GET: Menus1/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Menu menu = await _context.Menus.SingleAsync(m => m.Id == id);
            if (menu == null)
            {
                return HttpNotFound();
            }

            return View(menu);
        }

        // POST: Menus1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Menu menu = await _context.Menus.SingleAsync(m => m.Id == id);
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
