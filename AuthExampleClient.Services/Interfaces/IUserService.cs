using AuthExampleClient.DTOs.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<RoleDto>> GetUserRolesAsync(string token);
    }
}
