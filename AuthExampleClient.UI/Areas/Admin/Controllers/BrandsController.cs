using AspNetCoreHero.ToastNotification.Abstractions;
using AuthExampleClient.DTOs.Brand;
using AuthExampleClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Brands")]
    public class BrandsController : BaseController
    {
        private readonly IReadService<Brand> _readService;
        private readonly IWriteService<CreateBrand, UpdateBrand> _writeService;

        public BrandsController(IReadService<Brand> readService, IWriteService<CreateBrand, UpdateBrand> writeService, INotyfService notyfService)
            : base(notyfService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Index()
        {
            var datas = await _readService.GetAllAsync("Brands/GetAllBrands", "brands");
            return View(datas);
        }

        [HttpGet("[action]")]
        public IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateBrand(CreateBrand createBrand)
        {
            return await HandleFormAndApiRequestAsync(
                createBrand,
                () => _writeService.CreateAsync("Brands/CreateBrand", createBrand),
                "Marka başarıyla eklendi.",
                "Marka eklenirken bir hata oluştu.",
                "CreateBrand"
            );
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            var brand = await _readService.GetAsync("Brands/GetByIdBrand/", id);
            return View(new UpdateBrand
            {
                Id = brand.Id,
                BrandName = brand.BrandName
            });
        }

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> UpdateBrand(UpdateBrand updateBrand)
        {
            return await HandleFormAndApiRequestAsync(
                updateBrand,
                () => _writeService.UpdateAsync("Brands/UpdateBrand/", updateBrand),
                "Marka başarıyla güncellendi.",
                "Marka güncellenirken bir hata oluştu.",
                "UpdateBrand"
            );
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            return await HandleDeleteRequestAsync(
                id,
                id => _writeService.DeleteAsync("Brands/RemoveBrand/", id),
                "Marka başarıyla silindi.",
                "Marka silinirken bir hata oluştu."
            );
        }

    }
}
