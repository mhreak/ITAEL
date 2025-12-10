using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class ApplicantExamAttemptController(IExamService examService,
                                                IApplicantService applicantService,
                                                IJobAnnouncementService jobAnnouncementService,
                                                IApplicantExamAttemptService applicantExamAttemptService,
                                                IJobAnnouncement_Exam_Service ja_Exam_Service) : BaseController
    {
        [Route("Index/{applicantId}/{jobAnnouncementId}")]
        public IActionResult Index(int applicantId, int jobAnnouncementId)
        {
            var applicantViewModel = applicantService.Get(applicantId);

            if (applicantViewModel == null)
            {
                ShowDangerToast(null, "داوطلب یافت نشد.");
                return RedirectToAction("ApplicantIndex", "Applicant_JobAnnouncement_", new { Area = "Admin", applicantId = applicantId });
            }

            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                ShowDangerToast(null, "آگهی یافت نشد.");
                return RedirectToAction("ApplicantIndex", "Applicant_JobAnnouncement_", new { Area = "Admin", applicantId = applicantId });
            }

            ViewBag.ApplicantId = applicantViewModel.ApplicantId;
            ViewBag.ApplicantFullName = applicantViewModel.FullName;
            ViewBag.JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId;
            ViewBag.JobAnnouncementTitle = jobAnnouncementViewModel.Title;

            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, int applicantId, int jobAnnouncementId)
        {
            //Paging and Sorting

            int currentPage = request.Page;
            int pageSize = request.PageSize;

            var applicantViewModel = applicantService.Get(applicantId);

            if (applicantViewModel == null)
            {
                ShowDangerToast(null, "کاربر یافت نشد.");
                return RedirectToAction("ApplicantIndex", "Applicant_JobAnnouncement_", new { Area = "Admin", applicantId = applicantId });
            }

            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                ShowDangerToast(null, "آگهی یافت نشد.");
                return RedirectToAction("ApplicantIndex", "Applicant_JobAnnouncement_", new { Area = "Admin", applicantId = applicantId });
            }

            var applicantExamAttemptViewModelList = new List<ApplicantExamAttemptViewModel>();
            var ja_Exam_ViewModelList = ja_Exam_Service.GetAllByJobAnnouncementId(jobAnnouncementViewModel.JobAnnouncementId);

            if (ja_Exam_ViewModelList != null && ja_Exam_ViewModelList.Any())
            {
                foreach (var ja_Exam_ViewModel in ja_Exam_ViewModelList)
                {
                    var examViewModel = examService.Get(ja_Exam_ViewModel.ExamId);
                    var applicantExamAttemptViewModel = applicantExamAttemptService.GetByApplicantIdAndExamId(applicantViewModel.ApplicantId, examViewModel.ExamId);
                    applicantExamAttemptViewModelList.Add(applicantExamAttemptViewModel);
                }
            }
            

            var result = new DataSourceResult()
            {
                Data = applicantExamAttemptViewModelList,
                Total = applicantExamAttemptViewModelList.Count // Total number of records
            };

            return Json(result);
        }

        //[HttpGet]
        //[Route("Create")]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[Route("Create")]
        //[ValidateAntiForgeryToken]
        //public virtual ActionResult Create(ExamViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (examService.Add(model) != -1)
        //        {
        //            ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
        //        }
        //    }
        //    else
        //    {
        //        var errors = ModelState
        //        .Where(x => x.Value.Errors.Count > 0)
        //        .Select(x => new { x.Key, x.Value.Errors })
        //        .ToArray();

        //        ShowDangerToast(null, "اطلاعات واردشده معتبر نیست");
        //    }
        //    return View(model);
        //}

        //[HttpGet]
        //[Route("Edit/{id}")]
        //public IActionResult Edit(int id)
        //{
        //    var model = examService.Get(id);
        //    return View(model);
        //}

        //[HttpPost]
        //[Route("Edit/{id}")]
        //[ValidateAntiForgeryToken]
        //public virtual ActionResult Edit(ExamViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (examService.Edit(model))
        //        {
        //            ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
        //        }
        //    }
        //    else
        //    {
        //        var errors = ModelState
        //        .Where(x => x.Value.Errors.Count > 0)
        //        .Select(x => new { x.Key, x.Value.Errors })
        //        .ToArray();

        //        ShowDangerToast(null, "اطلاعات واردشده معتبر نیست");
        //    }
        //    return View(model);
        //}

        [Route("Delete")]
        public bool Delete(int id)
        {
            return applicantExamAttemptService.Delete(id);
        }
    }
}
