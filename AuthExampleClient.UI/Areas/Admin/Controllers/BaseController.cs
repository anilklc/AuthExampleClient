﻿using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

public class BaseController : Controller
{
    protected readonly INotyfService notyfService;

    public BaseController(INotyfService notyfService)
    {
        this.notyfService = notyfService;
    }

    protected async Task<IActionResult> HandleFormAndApiRequestAsync<TModel>(TModel model, Func<Task<bool>> apiCall, string successMessage, string errorMessage, string viewName = null)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            foreach (var error in errors)
            {
                notyfService.Error(error);
            }

            return View(viewName ?? nameof(model), model);
        }

        var apiResponse = await apiCall();

        if (apiResponse)
        {
            notyfService.Success(successMessage);
            return new OkResult(); // Başarılı durumda özel bir sonuç döndür
        }
        else
        {
            notyfService.Error(errorMessage);
            return new BadRequestResult(); // Hata durumunda özel bir sonuç döndür
        }
    }

    protected async Task<IActionResult> HandleDeleteRequestAsync(string id, Func<string, Task<bool>> apiCall, string successMessage, string errorMessage)
    {
        var apiResponse = await apiCall(id);

        if (apiResponse)
        {
            notyfService.Success(successMessage);
        }
        else
        {
            notyfService.Error(errorMessage);
        }

        return RedirectToAction("Index");
    }

}
