using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    public class JobAnnouncement_ExamResource_Controller(IJobAnnouncement_ExamResource_Service ja_ExamResource_Service) : BaseController
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
            //var jobAnnouncement_Exam_ViewModelList = ja_ExamResource_Service.GetAllFiltered(filterJobAnnouncementId, filterExamId,
            //                                                                                filterStartTimeFrom, filterStartTimeTo,
            //                                                                                filterEndTimeFrom, filterEndTimeTo,
            //                                                                                null, null, null, null,
            //                                                                                request.Page, request.PageSize, out int totalRecord);

            var result = new DataSourceResult()
            {
                //Data = jobAnnouncement_Exam_ViewModelList,
                //Total = jobAnnouncement_Exam_ViewModelList.Count // Total number of records
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
                if (!ja_ExamResource_Service.IsDuplicate(model.JobAnnouncementId, model.ExamId))
                {
                    if (ja_ExamResource_Service.Add(model.JobAnnouncementId, model.ExamId))
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
        [Route("Edit/{jobAnnouncementId}/{examResourceId}")]
        public IActionResult Edit(int jobAnnouncementId, int examResourceId)
        {
            var model = ja_ExamResource_Service.Get(jobAnnouncementId, examResourceId);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(JobAnnouncement_ExamResource_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ja_ExamResource_Service.Edit(model))
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
            return ja_ExamResource_Service.Delete(jobAnnouncementId, examId);
        }

        [Route("Fill_Ja_ExamResource_Combo")]
        public virtual JsonResult Fill_Ja_ExamResource_Combo(int jobAnnouncementId)
        {
            var DataList = ja_ExamResource_Service.GetAllByJobAnnouncementId(jobAnnouncementId)
                                                  .Select(x =>
                                                              new SelectListItem
                                                              {
                                                                  Text = x.ResourceName,
                                                                  Value = x.ExamResourceId.ToString()
                                                              });
            return Json(DataList);
        }
    }
}
