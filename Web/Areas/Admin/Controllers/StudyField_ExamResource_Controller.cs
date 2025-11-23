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
    public class StudyField_ExamResource_Controller(IStudyField_ExamResource_Service studyField_ExamResource_Service, IStudyFieldService studyFieldService) : BaseController
    {
        [Route("Index/{studyFieldId}")]
        public IActionResult Index(int studyFieldId)
        {
            var studyFieldViewModel = studyFieldService.Get(studyFieldId);

            if (studyFieldViewModel == null)
            {
                ShowDangerToast("", "رشته تحصیلی یافت نشد");
            }

            ViewBag.StudyFieldId = studyFieldViewModel.StudyFieldId;
            ViewBag.StudyFieldName = studyFieldViewModel.StudyFieldName;
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
                                            string filterStudyFieldId, string filterExamResourceId,
                                            string filterInsertDateFrom, string filterInsertDateTo)
        {
            var result = new DataSourceResult()
            {
                Data = studyField_ExamResource_Service.GetAllFiltered(filterStudyFieldId, filterExamResourceId,
                                                                      filterInsertDateFrom, filterInsertDateTo,
                                                                      request.Page, request.PageSize, out int totalRecord),
                Total = totalRecord // Total number of records
            };

            return Json(result);
        }

        [HttpGet]
        [Route("Create/{studyFieldId}")]
        public IActionResult Create(int studyFieldId)
        {
            var studyFieldViewModel = studyFieldService.Get(studyFieldId);

            if (studyFieldViewModel == null)
            {
                ShowDangerToast("", "رشته تحصیلی یافت نشد");
            }

            ViewBag.StudyFieldId = studyFieldViewModel.StudyFieldId;
            ViewBag.StudyFieldName = studyFieldViewModel.StudyFieldName;

            return View();
        }

        [HttpPost]
        [Route("Create/{studyFieldId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudyField_ExamResource_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!studyField_ExamResource_Service.IsDuplicate(model.ExamResourceId, model.StudyFieldId))
                {
                    if (studyField_ExamResource_Service.Add(model.ExamResourceId, model.StudyFieldId))
                    {
                        ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                        return RedirectToAction("Index", new { studyFieldId = model.StudyFieldId });
                    }
                    else
                    {
                        ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                        return RedirectToAction("Index", new { studyFieldId = model.StudyFieldId });
                    }
                }
                else
                {
                    ShowDangerToast(null, "این منبع آزمون برای این رشته تحصیلی وجود دارد");
                    return RedirectToAction("Index", new { studyFieldId = model.StudyFieldId });
                }
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowDangerToast(null, "اطلاعات واردشده معتبر نیست");
                return RedirectToAction("Index", new { studyFieldId = model.StudyFieldId });
            }
            return View(model);
        }

        [HttpGet]
        [Route("Edit/{studyFieldId}/{examResourceId}")]
        public IActionResult Edit(int studyFieldId, int examResourceId)
        {
            var studyFieldExamResourceViewModel = studyField_ExamResource_Service.Get(examResourceId, studyFieldId);

            if (studyFieldExamResourceViewModel == null)
            {
                ShowDangerToast("", "رشته تحصیلی یافت نشد");
            }

            return View(studyFieldExamResourceViewModel);
        }

        [HttpPost]
        [Route("Edit/{studyFieldId}/{examResourceId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StudyField_ExamResource_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (studyField_ExamResource_Service.Edit(model))
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index", new { studyFieldId = model.StudyFieldId });
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
        public bool Delete(int studyFieldId, int examResourceId)
        {
            return studyField_ExamResource_Service.Delete(examResourceId, studyFieldId);
        }

        [Route("Fill_StudyField_ExamResource_Combo")]
        public virtual JsonResult Fill_StudyField_ExamResource_Combo(int skillId)
        {
            var DataList = studyField_ExamResource_Service.GetAllByStudyFieldId(skillId)
                                                          .Select(x =>
                                                                      new SelectListItem
                                                                      {
                                                                          Text = x.StudyFieldName,
                                                                          Value = x.StudyFieldId.ToString()
                                                                      });
            return Json(DataList);
        }
    }
}
