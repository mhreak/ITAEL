using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.Controllers;
using Web.Model;
using Web.Service;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ExamQuestionController(IExamQuestionService examQuestionService, IExamService examService) : BaseController
    {
        [Route("Index/{examId}")]
        public IActionResult Index(int examId)
        {
            var examViewModel = examService.Get(examId);

            if (examViewModel == null)
            {
                ShowDangerToast("", "آزمون یافت نشد.");
            }

            ViewBag.ExamId = examViewModel.ExamId;
            ViewBag.ExamTitle = examViewModel.Title;

            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, string filterText,
                                            string filterExamId, string filterType)
        {
            //Paging and Sorting

            int currentPage = request.Page;
            int pageSize = request.PageSize;

            var result = new DataSourceResult()
            {
                Data = examQuestionService.GetAllFiltered(filterText, filterExamId, null,
                                                          filterType, null, currentPage, 
                                                          pageSize, out int totalRecord),
                Total = totalRecord // Total number of records
            };

            return Json(result);
        }

        [HttpGet]
        [Route("Create/{examId}")]
        public IActionResult Create(int examId)
        {
            var examViewModel = examService.Get(examId);

            if (examViewModel == null)
            {
                ShowDangerToast("", "آزمون یافت نشد.");
                return RedirectToAction("Index");
            }

            ViewBag.ExamId = examId;
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExamQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (examQuestionService.Add(model) != -1)
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
            var model = examQuestionService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(ExamQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (examQuestionService.Edit(model))
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
            return examQuestionService.Delete(id);
        }
    }
}
