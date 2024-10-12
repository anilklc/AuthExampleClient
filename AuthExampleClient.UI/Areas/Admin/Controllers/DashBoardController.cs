using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Dashboard")]
    public class DashBoardController : Controller
    {

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
