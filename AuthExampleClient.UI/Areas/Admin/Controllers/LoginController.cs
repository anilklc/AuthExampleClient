using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.Login;
using AuthExampleClient.DTOs.Product;
using AuthExampleClient.Services.Interfaces;
using AuthExampleClient.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Login")]
    public class LoginController : BaseController
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService,INotyfService notyfService) : base(notyfService) 
        {
            _authService = authService;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Index(Login login)
        {
            // API çağrısını yap ve sonucu al
            var result = await HandleFormAndApiRequestAsync(login,
                () => _authService.AuthenticateAsync("Auth/Login/", login),
                "Giriş başarılı",
                "Giriş başarısız",
                nameof(Index));

          if(result is OkResult)
            {
                return RedirectToAction("Index", "DashBoard");
            }
            else
            {
                return View(login);
            }
        }



        public async Task<IActionResult> Logout()
        {
            _authService.RemoveTokenFromCookie();
            return RedirectToAction("Index");
            
        }

    }
}
