﻿using Microsoft.AspNetCore.Mvc;

namespace AuthExampleClient.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Products")]
    public class ProductsController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}