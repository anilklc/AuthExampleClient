using AuthExampleClient.DTOs.Role;
using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IReadService<RoleDto> _readService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IReadService<RoleDto> readService, IHttpContextAccessor httpContextAccessor)
        {
            _readService = readService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<RoleDto>> GetUserRolesAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                
                string? name = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                var roles =await _readService.GetAllAsync($"Users/GetRolesToUser/{name}", "roles");
                _httpContextAccessor.HttpContext?.Session.SetString("UserName", name);
                _httpContextAccessor.HttpContext?.Session.SetString("UserRoles", string.Join(",", roles.Select(r => r.Name)));
                return roles.ToList();
            }
            else 
            {
                return new List<RoleDto>();
            }
            
        }

    }
}
