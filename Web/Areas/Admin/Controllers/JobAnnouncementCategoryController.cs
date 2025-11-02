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
    public class JobAnnouncementCategoryController(IJobAnnouncementCategoryService jobAnnouncementCategoryService) : BaseController
    {
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            string filterCategoryName, string filterActive)
        {
            //Paging and Sorting
            int currentPage = request.Page;
            int pageSize = request.PageSize;

            int totalRecord = 0;


            var result = new DataSourceResult()
            {
                Data = jobAnnouncementCategoryService.GetAllFiltered(filterCategoryName, filterActive,
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
        public virtual ActionResult Create(JobAnnouncementCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!jobAnnouncementCategoryService.IsDuplicateByName(null, model.CategoryName))
                {
                    if (jobAnnouncementCategoryService.Add(model) != -1)
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
                    ShowDangerToast(null, "یک دسته‌بندی دیگر با این نام وجود دارد");
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
            var model = jobAnnouncementCategoryService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(JobAnnouncementCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!jobAnnouncementCategoryService.IsDuplicateByName(model.JobAnnouncementCategoryId, model.CategoryName))
                {
                    if (jobAnnouncementCategoryService.Edit(model))
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
                    ShowDangerToast(null, "یک دسته‌بندی دیگر با این نام وجود دارد");
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
            return jobAnnouncementCategoryService.Delete(id);
        }

        [Route("Fill_JobAnnouncementCategory_Combo")]
        public virtual JsonResult Fill_JobAnnouncementCategory_Combo(bool? active)
        {
            var DataList = jobAnnouncementCategoryService.GetAll(active)
                .Select(x =>
                new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.JobAnnouncementCategoryId.ToString()
                });
            return Json(DataList);
        }
    }
}
