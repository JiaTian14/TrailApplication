using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
        public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult signup()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Trail()
        {
            return View();
        }

        public IActionResult Activitytrail()
        {
            return View();
        }

        public IActionResult Viewtrail()
        {
            return View();
        }
        public IActionResult Favorite()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
