using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GrooveMessengerAPI.Models;
using Microsoft.Extensions.Logging;
using System;

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

            throw new IndexOutOfRangeException();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
