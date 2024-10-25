using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.Brand;
using AuthExampleClient.DTOs.Product;
using AuthExampleClient.Services.Attributes;
using AuthExampleClient.Services.Interfaces;
using AuthExampleClient.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Dashboard")]
    public class DashBoardController : Controller
    {
        private readonly IReadService<ProductStatistics> _productStatisticsService;
        public DashBoardController(IReadService<ProductStatistics> productStatisticsService)
        {
            _productStatisticsService = productStatisticsService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get Product Count", "Admin")]
        public async Task<IActionResult> Index()
        {
            var datas = await _productStatisticsService.GetAsync("Products/GetProductCount", "");
            return View(datas);
        }
    }
}
