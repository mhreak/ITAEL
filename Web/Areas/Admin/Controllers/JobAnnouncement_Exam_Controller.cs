using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class JobAnnouncement_Exam_Controller(IJobAnnouncement_Exam_Service ja_Exam_Service) : BaseController
    {
        [Route("Index/{jobAnnouncementId}")]
        public IActionResult Index(int jobAnnouncementId)
        {
            ViewBag.jobAnnouncementId = jobAnnouncementId;
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
                                            string filterJobAnnouncementId, string filterExamId,
                                            string filterStartTimeFrom, string filterStartTimeTo,
                                            string filterEndTimeFrom, string filterEndTimeTo)
        {
            var jobAnnouncement_Exam_ViewModelList = ja_Exam_Service.GetAllFiltered(filterJobAnnouncementId, filterExamId,
                                                                                    filterStartTimeFrom, filterStartTimeTo,
                                                                                    filterEndTimeFrom, filterEndTimeTo,
                                                                                    null, null, null, null,
                                                                                    request.Page, request.PageSize, out int totalRecord);

            var result = new DataSourceResult()
            {
                Data = jobAnnouncement_Exam_ViewModelList,
                Total = jobAnnouncement_Exam_ViewModelList.Count // Total number of records
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
        public virtual ActionResult Create(JobAnnouncement_Exam_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!ja_Exam_Service.IsDuplicate(model.JobAnnouncementId, model.ExamId))
                {
                    if (ja_Exam_Service.Add(model.JobAnnouncementId, model.ExamId))
                    {
                        ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                        return RedirectToAction("Index", new { jobAnnouncementId = model.JobAnnouncementId });
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
        [Route("Edit/{jobAnnouncementId}/{examId}")]
        public IActionResult Edit(int jobAnnouncementId, int examId)
        {
            var model = ja_Exam_Service.Get(jobAnnouncementId, examId);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(JobAnnouncement_Exam_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ja_Exam_Service.Edit(model))
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index", new { jobAnnouncementId = model.JobAnnouncementId });
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
        public bool Delete(int jobAnnouncementId, int examId)
        {
            return ja_Exam_Service.Delete(jobAnnouncementId, examId);
        }

        [Route("Fill_Ja_Exam_Combo")]
        public virtual JsonResult Fill_Ja_Exam_Combo(int jobAnnouncementId)
        {
            var DataList = ja_Exam_Service.GetAllByJobAnnouncementId(jobAnnouncementId)
                                          .Select(x =>
                                                      new SelectListItem
                                                      {
                                                          Text = x.ExamTitle,
                                                          Value = x.ExamId.ToString()
                                                      });
            return Json(DataList);
        }
    }
}
