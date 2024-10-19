using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.AuthorizeMenu;
using AuthExampleClient.Services.Interfaces;
using AuthExampleClient.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AuthorizeMenu")]
    public class AuthorizeMenuController : Controller
    {
        private readonly IReadService<ApplicationService> _readService;
        private readonly INotyfService _notyfService;
        public AuthorizeMenuController(IReadService<ApplicationService> readService, INotyfService notyfService)
        {
            _readService = readService;
            _notyfService = notyfService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("ApplicationServices/GetAuthorizeDefinitionEndpoints","");
            return View(datas);
        }
    }
}
