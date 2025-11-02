using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Web.Controllers;
using Web.Model;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class CompanyController(ICompanyService companyService) : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterCompanyName, string filterInsertDateFrom, string filterInsertDateTo)
        {
            //Paging and Sorting
            
            int currentPage = request.Page;
            int pageSize = request.PageSize;

            int totalRecord = 0;


            var result = new DataSourceResult()
            {
                Data = companyService.GetAllFiltered(filterCompanyName, filterInsertDateFrom, filterInsertDateTo, currentPage, pageSize, out totalRecord),
                Total = totalRecord // Total number of records
            };

            return Json(result);
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
        public virtual ActionResult Create(CompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!companyService.IsDuplicateByCompanyName(null, model.CompanyName))
                {
                    if (companyService.Add(model) != -1)
                    {
                        ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                    }
                }
                else
                {
                    ShowDangerToast(null, "یک شرکت دیگر با این نام وجود دارد");
                }
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowDangerToast(null, "اطلاعات واردشده معتبر نیست");
            }
            return View(model);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var model = companyService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(CompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!companyService.IsDuplicateByCompanyName(model.CompanyId, model.CompanyName))
                {
                    if (companyService.Edit(model))
                    {
                        ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                    }
                }
                else
                {
                    ShowDangerToast(null, "یک شرکت دیگر با این نام وجود دارد");
                }
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowDangerToast(null, "اطلاعات واردشده معتبر نیست");
            }
            return View(model);
        }

        [Route("Delete")]
        public bool Delete(int id)
        {
            return companyService.Delete(id);
        }

        [Route("Fill_Company_Combo")]
        public virtual JsonResult Fill_Company_Combo()
        {
            var DataList = companyService.GetAll()
                .Select(x =>
                new SelectListItem
                {
                    Text = x.CompanyName,
                    Value = x.CompanyId.ToString()
                });
            return Json(DataList);
        }
    }
}
