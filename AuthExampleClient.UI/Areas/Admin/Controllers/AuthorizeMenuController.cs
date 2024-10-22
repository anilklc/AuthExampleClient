using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.AuthorizeMenu;
using AuthExampleClient.DTOs.Brand;
using AuthExampleClient.DTOs.Role;
using AuthExampleClient.Services.Interfaces;
using AuthExampleClient.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AuthorizeMenu")]
    public class AuthorizeMenuController : BaseController
    {
        private readonly IReadService<ApplicationService> _readService;
        private readonly IReadService<Role> _roleService;
        private readonly IWriteService<AssignRole, AssignRole> _writeService;

        public AuthorizeMenuController(IReadService<ApplicationService> readService, IReadService<Role> roleService, INotyfService notyfService, IWriteService<AssignRole, AssignRole> writeService) : base(notyfService)
        {
            _readService = readService;
            _roleService = roleService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("ApplicationServices/GetAuthorizeDefinitionEndpoints", "");
            return View(datas);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRolesForMenu(string menu, string code)
        {
            var allRoles = await _roleService.GetAllAsync("Roles/GetAllRoles", "roles");
            menu = menu.Replace(" ", "");
            var assignedRoles = await _roleService.GetAllAsync($"AuthorizationEndpoints/GetRolesToEndpoint/{menu}/{code}", "roles");
            var assignedRoleIds = assignedRoles?.Select(r => r.Id).ToArray() ?? new string[] { };
            var result = new
            {
                AllRoles = allRoles,
                AssignedRoleIds = assignedRoleIds
            };
            return Json(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AssignRoles([FromBody]AssignRole assignRole)
        {
           return await HandleFormAndApiRequestAsync(
                assignRole,
                () => _writeService.CreateAsync("AuthorizationEndpoints/AssignRoleEndpoint", assignRole),
                "Roller başarıyla güncellendi.",
                "Roller güncellenirken bir hata oluştu."
            );

            
        }
    }
}
