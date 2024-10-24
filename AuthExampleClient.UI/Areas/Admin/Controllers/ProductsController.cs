using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.Product;
using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Products")]
    public class ProductsController : BaseController
    {
        private readonly IReadService<Product> _readService;
        private readonly IWriteService<CreateProduct, UpdateProduct> _writeService;

        public ProductsController(IReadService<Product> readService, IWriteService<CreateProduct, UpdateProduct> writeService, INotyfService notyfService)
            : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var products = await _readService.GetAllAsync("Products/GetAllProducts", "products");
            return View(products);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.brandValues = await GetBrandSelectList();
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProduct(CreateProduct createProduct)
        {
            ViewBag.brandValues = await GetBrandSelectList();

            return await HandleFormAndApiRequestAsync(
                createProduct,
                () => _writeService.CreateAsync("Products/CreateProduct", createProduct),
                "Ürün başarıyla eklendi.",
                "Ürün eklenirken bir hata oluştu.",
                "CreateProduct"
            );
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ViewBag.brandValues = await GetBrandSelectList();
            var product = await _readService.GetAsync("Products/GetByIdProduct/", id);

            return View(new UpdateProduct
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                BrandId = product.BrandId.ToString(),
            });
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProduct updateProduct)
        {
            ViewBag.brandValues = await GetBrandSelectList();
            return await HandleFormAndApiRequestAsync(
                updateProduct,
                () => _writeService.UpdateAsync("Products/UpdateProduct/", updateProduct),
                "Ürün başarıyla güncellendi.",
                "Ürün güncellenirken bir hata oluştu.",
                "UpdateProduct"
            );
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return await HandleDeleteRequestAsync(
                id,
                id => _writeService.DeleteAsync("Products/RemoveProduct/", id),
                "Ürün başarıyla silindi.",
                "Ürün silinirken bir hata oluştu."
            );
        }


        private async Task<List<SelectListItem>> GetBrandSelectList()
        {
            var brands = await _readService.GetAllAsync("Brands/GetAllBrands", "brands");
            return brands.Select(x => new SelectListItem
            {
                Text = x.BrandName,
                Value = x.Id.ToString()
            }).ToList();
        }
    }
}
