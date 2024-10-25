using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.Brand;
using AuthExampleClient.DTOs.Login;
using AuthExampleClient.DTOs.User;
using AuthExampleClient.Services.Attributes;
using AuthExampleClient.Services.Interfaces;
using AuthExampleClient.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Controllers
{
    [Route("Login")]

    public class LoginController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IWriteService<CreateUserDto,CreateUserDto> _writeService;

        public LoginController(IAuthService authService, INotyfService notyfService, IWriteService<CreateUserDto, CreateUserDto> writeService) : base(notyfService)
        {
            _authService = authService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            if (await _authService.HasTokenInCookie())
            {
                return RedirectToAction("Index", "Orders");
            }

            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Index(Login login)
        {
            return await HandleFormAndApiRequestAsync(
                login,
                () => _authService.AuthenticateAsync("Auth/Login/", login),
                "Giriş başarılı",
                "Giriş başarısız",
                nameof(Index));
        }

        public IActionResult Logout()
        {
            _authService.RemoveTokenFromCookie();
            return RedirectToAction("Index");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(CreateUserDto createUser)
        {
            return await HandleFormAndApiRequestAsync(
                createUser,
                () => _writeService.CreateAsync("Users/CreateUser", createUser),
                "Hesabınız oluşturuldu.",
                "Hesabınız oluşturulamadı.",
                "Index"
            );
        }
    }
}
