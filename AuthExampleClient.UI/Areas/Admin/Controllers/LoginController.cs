using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Login")]
    public class LoginController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
