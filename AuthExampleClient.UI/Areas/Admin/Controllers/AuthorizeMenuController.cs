using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AuthorizeMenu")]
    public class AuthorizeMenuController : Controller
    {
        [HttpGet("[action]")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
