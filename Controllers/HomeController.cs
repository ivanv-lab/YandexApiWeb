using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YandexApiWeb.Models;

namespace YandexApiWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static bool _isProcessing=false;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Run(string code)
        {
            var authManager = new AuthManager();
            string oauthToken = await authManager.GetToken(code);
            var diskManager = new YandexFileManager(oauthToken);
            await diskManager.Run();
            return View("Done"); 
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
