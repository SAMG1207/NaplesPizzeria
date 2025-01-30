using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NapplesPizzeria.Models;
using NapplesPizzeria.Services;

namespace NapplesPizzeria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OwnerServices _ownerServices;
        
        public HomeController (ILogger<HomeController> logger, OwnerServices ownerServices)
        {
            _logger = logger;
            _ownerServices = ownerServices;
           
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string username, string password)
        {
            string result = _ownerServices.validateCredentials(username, password);
            if (result == "OK")
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.ErrorMessage = result;
                return View();
            }
            
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
