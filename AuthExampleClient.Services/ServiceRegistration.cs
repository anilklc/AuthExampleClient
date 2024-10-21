using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using AuthExampleClient.Services.Interfaces;
using AuthExampleClient.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthExampleClient.Services
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHttpClient("AuthExampleApi",client =>
            {
                client.BaseAddress = new Uri(configuration.GetSection("BaseUrl").Value);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
             AddJwtBearer("Admin", options =>
             {
                 options.TokenValidationParameters = new()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,

                     ValidAudience = configuration["Token:Audience"],
                     ValidIssuer = configuration["Token:Issuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"])),
                     LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
                     NameClaimType = ClaimTypes.Name,
                     RoleClaimType = ClaimTypes.Role,

                 };
             });

            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped(typeof(IReadService<>), typeof(ReadService<>));
            services.AddScoped(typeof(IWriteService<,>), typeof(WriteService<,>));
        }
    }
}
