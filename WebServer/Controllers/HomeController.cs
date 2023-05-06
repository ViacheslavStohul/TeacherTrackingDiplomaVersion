using BusinessCore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserServise _userService;

        public HomeController(ILogger<HomeController> logger, IUserServise userService)
        {
            _logger = logger;
            this._userService = userService;
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}