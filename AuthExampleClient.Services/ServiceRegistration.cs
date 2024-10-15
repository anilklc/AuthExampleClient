using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using AuthExampleClient.Services.Interfaces;
using AuthExampleClient.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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

            services.AddScoped(typeof(IReadService<>), typeof(ReadService<>));
            services.AddScoped(typeof(IWriteService<>), typeof(WriteService<>));
        }
    }
}
