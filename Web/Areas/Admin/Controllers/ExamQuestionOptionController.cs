using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class ExamQuestionOptionController(IExamQuestionOptionService examQuestionOptionService) : BaseController
    {
        [Route("Index/{examQuestionId}")]
        public IActionResult Index(int examQuestionId)
        {
            ViewBag.ExamQuestionId = examQuestionId;
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, string filterTitle, string filterExamQuestionId)
        {
            //Paging and Sorting

            int currentPage = request.Page;
            int pageSize = request.PageSize;

            var result = new DataSourceResult()
                         {
                             Data = examQuestionOptionService.GetAllFiltered(filterTitle, filterExamQuestionId, null,
                                                                             null, currentPage, pageSize, out int totalRecord),
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
        public virtual ActionResult Create(ExamQuestionOptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (examQuestionOptionService.Add(model) != -1)
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
            var model = examQuestionOptionService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(ExamQuestionOptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (examQuestionOptionService.Edit(model))
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
            return examQuestionOptionService.Delete(id);
        }
    }
}
