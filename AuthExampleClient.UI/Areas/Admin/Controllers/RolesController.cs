﻿using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.Role;
using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Roles")]
    public class RolesController : BaseController
    {
        private readonly IReadService<Role> _readService;
        private readonly IWriteService<CreateRole, UpdateRole> _writeService;

        public RolesController(IReadService<Role> readService, IWriteService<CreateRole, UpdateRole> writeService, INotyfService notyfService)
            : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("Roles/GetAllRoles", "roles");
            return View(datas);
        }

        [HttpGet("[action]")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(CreateRole createRole)
        {
            return await HandleFormAndApiRequestAsync(
                createRole,
                () => _writeService.CreateAsync("Roles/CreateRole", createRole),
                "Rol başarıyla eklendi.",
                "Rol eklenirken bir hata oluştu.",
                "CreateRole"
            );
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateRole(string id)
        {
            var role = await _readService.GetAsync("Roles/GetByIdRole/", id);
            return View(new UpdateRole
            {
                Id = role.Id,
                Name = role.Name
            });
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateRole(UpdateRole updateRole)
        {
            return await HandleFormAndApiRequestAsync(
                updateRole,
                () => _writeService.UpdateAsync("Roles/UpdateRole/", updateRole),
                "Rol başarıyla güncellendi.",
                "Rol güncellenirken bir hata oluştu.",
                "UpdateRole"
            );
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            return await HandleDeleteRequestAsync(
                id,
                id => _writeService.DeleteAsync("Roles/RemoveRole/", id),
                "Rol başarıyla silindi.",
                "Rol silinirken bir hata oluştu."
            );
        }
    }
}
