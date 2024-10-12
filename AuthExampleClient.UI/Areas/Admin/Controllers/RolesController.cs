using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Roles")]
    public class RolesController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
