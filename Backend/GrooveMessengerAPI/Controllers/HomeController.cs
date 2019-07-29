using System.Diagnostics;
using GrooveMessengerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrooveMessengerAPI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {
            logger.LogTrace("HomeController is created as new instance", typeof(HomeController));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}