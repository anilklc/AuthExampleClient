using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.Brand;
using AuthExampleClient.DTOs.Order;
using AuthExampleClient.Services.Attributes;
using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Orders")]
    public class OrdersController : BaseController
    {
        private readonly IReadService<Order> _readService;
        private readonly IWriteService<CreateOrder, UpdateOrder> _writeService;

        public OrdersController(IReadService<Order> readService, IWriteService<CreateOrder, UpdateOrder> writeService,INotyfService notyfService) : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        [AuthorizeRole("Get All Order", "Admin")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("Orders/GetAllOrders", "orders");
            return View(datas);
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeRole("Update Order", "Admin")]
        public async Task<IActionResult> UpdateOrder(string id)
        {
            var order = await _readService.GetAsync("Orders/GetByIdOrder/", id);
            return View(new UpdateOrder
            {
                Id = order.Id,
                Status = order.Status,
            });
        }

        [HttpPost("[action]/{id}")]
        [AuthorizeRole("Update Order", "Admin")]
        public async Task<IActionResult> UpdateOrder(UpdateOrder updateOrder)
        {
            return await HandleFormAndApiRequestAsync(
                updateOrder,
                () => _writeService.UpdateAsync("Orders/UpdateOrder/", updateOrder),
                "Sipariş başarıyla güncellendi.",
                "Sipariş güncellenirken bir hata oluştu.",
                "UpdateOrder"
            );
        }

    }
}
