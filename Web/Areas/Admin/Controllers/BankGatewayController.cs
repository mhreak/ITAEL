using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Kendo.Mvc.Extensions;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]   
    [Authorize(Roles = "Admin, Manager")]
    public class BankGatewayController(IBankGatewayService bankGatewayService) : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(bankGatewayService.GetAll(null).ToDataSourceResult(request));
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BankGatewayViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (bankGatewayService.Add(model) != -1)
                {
                    ShowSuccessToast(null, "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index");
                }
                else
                {
                    ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                }
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowDangerToast(null, "اطلاعات واردشده نامعتبر است");
            }
            return View(model);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var model = bankGatewayService.Get(id);
            if (model == null)
            {
                return null;
            }

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BankGatewayViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (bankGatewayService.Edit(model))
                {
                    ShowSuccessToast(null, "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index");
                }
                else
                {
                    ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                }
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowDangerToast(null, "اطلاعات واردشده نامعتبر است");
            }
            return View(model);
        }
    }
}
