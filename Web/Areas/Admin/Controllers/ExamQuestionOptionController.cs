using DbEntities;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.Controllers;
using Web.Model;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ExamQuestionOptionController(IExamQuestionOptionService examQuestionOptionService, IExamQuestionService examQuestionService) : BaseController
    {
        [Route("Index/{examQuestionId}")]
        public IActionResult Index(int examQuestionId)
        {
            var examQuestionViewModel = examQuestionService.Get(examQuestionId);

            if (examQuestionViewModel == null)
            {
                ShowDangerToast("", "سوال آزمون یافت نشد.");
            }

            ViewBag.ExamId = examQuestionViewModel.ExamId;
            ViewBag.ExamQuestionId = examQuestionId;
            ViewBag.ExamQuestionOrder = examQuestionViewModel.QuestionOrder;
            ViewBag.ExamTitle = examQuestionViewModel.ExamTitle;
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
        [Route("Create/{examQuestionId}")]
        public IActionResult Create(int examQuestionId)
        {
            var examViewModel = examQuestionService.Get(examQuestionId);

            if (examViewModel == null)
            {
                ShowDangerToast("", "آزمون یافت نشد.");
                return RedirectToAction("Index", new { examQuestionId = examQuestionId });
            }

            ViewBag.ExamQuestionId = examQuestionId;
            return View();
        }

        [HttpPost]
        [Route("Create/{examQuestionId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExamQuestionOptionViewModel model, int examQuestionId)
        {
            if (ModelState.IsValid)
            {
                if (model.IsCorrectAnswer)
                {
                    var examQuestionOptionViewModel = examQuestionOptionService.GetCorrectAnswerByExamQuestionId(examQuestionId);

                    if (examQuestionOptionViewModel != null)
                    {
                        ShowSuccessToast(null, "قبلا گزینه صحیح انتخاب شده است.");
                        return RedirectToAction("Index", new { examQuestionId = model.ExamQuestionId });
                    }
                }
                
                if (examQuestionOptionService.Add(model) != -1)
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");
                    return RedirectToAction("Index", new { examQuestionId = examQuestionId });
                }
                else
                {
                    ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                    return RedirectToAction("Index", new { examQuestionId = model.ExamQuestionId });
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

            ViewBag.ExamQuestionId = examQuestionId;
            return View(model);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var examQuestionOptionViewModel = examQuestionOptionService.Get(id);

            if (examQuestionOptionViewModel == null)
            {
                ShowDangerToast("", "گزینه سوال آزمون یافت نشد.");
                return RedirectToAction("Index", new { examQuestionId = id });
            }
            return View(examQuestionOptionViewModel);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExamQuestionOptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsCorrectAnswer)
                {
                    var examQuestionOptionViewModel = examQuestionOptionService.GetCorrectAnswerByExamQuestionId(model.ExamQuestionId);

                    if (examQuestionOptionViewModel != null)
                    {
                        ShowWarningToast(null, "قبلا گزینه صحیح انتخاب شده است.");
                        return RedirectToAction("Index", new { examQuestionId = model.ExamQuestionId });
                    }
                }

                if (examQuestionOptionService.Edit(model))
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index", new { examQuestionId = model.ExamQuestionId });
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
