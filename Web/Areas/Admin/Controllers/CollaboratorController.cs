using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class CollaboratorController(ICollaboratorService collaboratorService) : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterReferralCode, string filterActive)
        {
            //Paging and Sorting
            int currentPage = request.Page;
            int pageSize = request.PageSize;

            int totalRecord = 0;


            var result = new DataSourceResult()
            {
                Data = collaboratorService.GetAllFiltered(filterReferralCode, filterActive,
                                                          currentPage, pageSize, out totalRecord),
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
        public virtual ActionResult Create(CollaboratorViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (collaboratorService.Add(model) != -1)
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
            var model = collaboratorService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(CollaboratorViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (collaboratorService.Edit(model))
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
            return collaboratorService.Delete(id);
        }
    }
}
