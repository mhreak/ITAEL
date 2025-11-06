using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class StudyField_ExamResource_Controller(IStudyField_ExamResource_Service studyField_ExamResource_Service) : BaseController
    {
        [Route("Index/{studyFieldId}")]
        public IActionResult Index(int studyFieldId)
        {
            ViewBag.StudyFieldId = studyFieldId;
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            int studyFieldId)
        {
            var studyField_ExamResource_ViewModelList = studyField_ExamResource_Service.GetAllByStudyFieldId(studyFieldId).ToList();

            var result = new DataSourceResult()
            {
                Data = studyField_ExamResource_ViewModelList,
                Total = studyField_ExamResource_ViewModelList.Count // Total number of records
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
        public virtual ActionResult Create(StudyField_ExamResource_ViewModel model)
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
                    }
                }
                else
                {
                    ShowDangerToast(null, "یک مهارت دیگر با این نام وجود دارد");
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
        [Route("Edit/{studyFieldId}/{examResourceId}")]
        public IActionResult Edit(int studyFieldId, int examResourceId)
        {
            var model = studyField_ExamResource_Service.Get(examResourceId, studyFieldId);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(StudyField_ExamResource_ViewModel model)
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
