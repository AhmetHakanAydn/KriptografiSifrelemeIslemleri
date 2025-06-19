using System.Diagnostics;
using ahmetkripto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ahmetkripto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var distPath = Path.Combine(_environment.ContentRootPath, "ClientApp", "dist", "index.html");
            if (System.IO.File.Exists(distPath))
            {
                return PhysicalFile(distPath, "text/html");
            }
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
