using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.AuthorizeMenu;
using AuthExampleClient.DTOs.Role;
using AuthExampleClient.DTOs.User;
using AuthExampleClient.Services.Attributes;
using AuthExampleClient.Services.Interfaces;
using AuthExampleClient.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Users")]
    public class UsersController : BaseController
    {
        private readonly IReadService<User> _readService;
        private readonly IReadService<Role> _roleService;
        private readonly IWriteService<AssingRoleToUser, AssingRoleToUser> _writeService;
        public UsersController(IReadService<User> readService, IReadService<Role> roleService, IWriteService<AssingRoleToUser, AssingRoleToUser> writeService, INotyfService notyfService) : base(notyfService)
        {
            _readService = readService;
            _roleService = roleService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get Authorize Definition", "Admin")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("Users/GetAllUsers","users");
            return View(datas);
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get Roles To User", "Admin")]
        public async Task<IActionResult> GetRolesForUser(string id)
        {
            var allRoles = await _roleService.GetAllAsync("Roles/GetAllRoles", "roles");
            var assignedRoles = await _roleService.GetAllAsync($"Users/GetRolesToUser/{id}", "roles");
            var assignedRoleNames = assignedRoles?.Select(r => r.Name).ToArray() ?? new string[] { };
            var result = new
            {
                AllRoles = allRoles,
                AssignedRoleNames = assignedRoleNames
            };
            return Json(result);
        }

        [HttpPost("[action]")]
        [AuthorizeRole("Assign Role To User", "Admin")]
        public async Task<IActionResult> AssignRolesToUser([FromBody] AssingRoleToUser assingRoleToUser)
        {
            return await HandleFormAndApiRequestAsync(
                 assingRoleToUser,
                 () => _writeService.CreateAsync("Users/AssignRoleToUser", assingRoleToUser),
                 "Roller başarıyla kullanıcıya eklendi.",
                 "Roller eklenirken bir hata oluştu."
             );


        }
    }
}
