using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.Brand;
using AuthExampleClient.DTOs.Order;
using AuthExampleClient.Services.Attributes;
using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Controllers
{
    [Route("Orders")]
    public class OrdersController : BaseController
    {
        private readonly IReadService<Order> _readService;
        private readonly IWriteService<CreateOrder, CreateOrder> _writeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersController(IReadService<Order> readService, IHttpContextAccessor httpContextAccessor, INotyfService notyfService, IWriteService<CreateOrder, CreateOrder> writeService) : base(notyfService)
        {
            _readService = readService;
            _httpContextAccessor = httpContextAccessor;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("User", "")]
        public async Task<IActionResult> Index()
        {
            var username = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            var datas = await _readService.GetAllAsync($"Orders/GetByUsernameOrder/{username}", "orders");
            return View(datas);
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("User", "")]
        public async Task<IActionResult> CreateOrder(string id)
        {
            CreateOrder createOrder = new()
            {
                Username = _httpContextAccessor.HttpContext.Session.GetString("UserName"),
                ProductId = id,
            };
            return await HandleFormAndApiRequestAsync(
                createOrder,
                () => _writeService.CreateAsync("Orders/CreateOrder", createOrder),
                "Ürün başarıyla satın alındı.",
                "Ürün alınamadı almak için giriş yapmanız gerekmektedir.",
                "Index"
            );
        }

    }
}

