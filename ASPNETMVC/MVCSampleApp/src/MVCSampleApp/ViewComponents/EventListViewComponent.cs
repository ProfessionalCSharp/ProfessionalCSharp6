using Microsoft.AspNetCore.Mvc;
using MVCSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSampleApp.ViewComponents
{
    public class EventListViewComponent : ViewComponent
    {
        private readonly EventsAndMenusContext _context;
        public EventListViewComponent(EventsAndMenusContext context)
        {

            _context = context;          
        }

        //public IViewComponentResult Invoke()
        //{
        //    return View(_context.Events);
        //}

        public Task<IViewComponentResult> InvokeAsync(DateTime from, DateTime to) =>
            Task.FromResult<IViewComponentResult>(View(EventsByDateRange(from, to)));

        private IEnumerable<Event> EventsByDateRange(DateTime from, DateTime to) =>
            _context.Events.Where(e => e.Day >= from && e.Day <= to);
      

    }
}
