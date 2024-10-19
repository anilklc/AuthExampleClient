using AuthExampleClient.DTOs.Product;
using AuthExampleClient.Services.Interfaces;
using AuthExampleClient.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuthExampleClient.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IReadService<ProductUser> _readService;
        public HomeController(ILogger<HomeController> logger, IReadService<ProductUser> readService)
        {
            _logger = logger;
            _readService = readService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _readService.GetAllAsync("Products/GetAllProducts", "products");
            return View(products);
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
