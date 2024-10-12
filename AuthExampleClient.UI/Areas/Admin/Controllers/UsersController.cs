using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Users")]
    public class UsersController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
