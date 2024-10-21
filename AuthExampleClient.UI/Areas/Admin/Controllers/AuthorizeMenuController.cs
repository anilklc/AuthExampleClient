using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.AuthorizeMenu;
using AuthExampleClient.DTOs.Role;
using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AuthorizeMenu")]
    public class AuthorizeMenuController : BaseController
    {
        private readonly IReadService<ApplicationService> _readService;
        private readonly IReadService<Role> _roleService;
 

        public AuthorizeMenuController(IReadService<ApplicationService> readService, IReadService<Role> roleService, INotyfService notyfService) : base(notyfService)
        {
            _readService = readService;
            _roleService = roleService; 
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("ApplicationServices/GetAuthorizeDefinitionEndpoints", "");
            var roles = await _roleService.GetAllAsync("Roles/GetAllRoles", "roles");
            ViewBag.Roles = roles;
            return View(datas);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AssignRoles(string[] roleIds)
        {
            return View();
        }
    }
}
