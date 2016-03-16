using Microsoft.AspNet.Mvc;
using MVCSampleApp.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCSampleApp.Controllers
{
    public class UseAServiceController : Controller
    {
        private ISampleService _sampleService;
        public UseAServiceController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        // GET: /<controller>/
        public IActionResult Index() => View();

        public string GetSampleStrings() =>
            string.Join(", ", _sampleService.GetSampleStrings());
    }
}
