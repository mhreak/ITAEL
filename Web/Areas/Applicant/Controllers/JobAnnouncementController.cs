using System;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Web.Model;
using Web.Service.Identity.Interface;
using Web.Service.Interface;

namespace Web.Areas.Applicant.Controllers
{
    [Area("Applicant")]
    [Route("Applicant/[controller]")]
    [Authorize]
    public class JobAnnouncementController(IJobAnnouncementService jobAnnouncementService,
                                           IApplicantService applicantService,
                                           IApplicationUserManagerService applicationUserManagerService,
                                           IApplicant_JobAnnouncement_Service applicant_JA_Service) : BaseController
    {
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            var jobAnnouncementViewModelList = jobAnnouncementService.GetAllLast(10);
            return View(jobAnnouncementViewModelList);
        }

        //[HttpGet]
        //[Route("Details/{jobAnnouncementId}")]
        //public IActionResult Details(int jobAnnouncementId)
        //{
        //    var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);
        //    return View(jobAnnouncementViewModel);
        //}

        [HttpPost]
        [Route("Request/{jobAnnouncementId}")]
        public IActionResult Request(JobAnnouncementRequestViewModel model)
        {
            var user = applicationUserManagerService.GetCurrentUser();
            var applicant = applicantService.Get(user.ApplicantId.Value);
            var jobAnnouncementViewModel = jobAnnouncementService.Get(model.jobAnnouncementId);

            if (applicant_JA_Service.IsDuplicate(applicant.ApplicantId, jobAnnouncementViewModel.JobAnnouncementId))
            {
                TempData["ErrorMessage"] = "درخواست این آگهی قبلا برای شما ثبت شده است.";
                return RedirectToAction("Index", "JobAnnouncement", new { Area = "Applicant" });
            }
            var aplicant_Ja_ViewModel = new Applicant_JobAnnouncement_ViewModel()
                                        {
                                            ApplicantId = applicant.ApplicantId,
                                            JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId,
                                            InsertDate = DateTime.Now,
                                            Status = 0
                                        };

            var isAdd = applicant_JA_Service.Add(aplicant_Ja_ViewModel);

            if (!isAdd)
            {
                TempData["ErrorMessage"] = "درخواست شما ثبت نشد لطفا بعدا دوباره تلاش کنید.";
            }

            return RedirectToAction("Index", "JobAnnouncement", new { Area = "Applicant" });
        }
    }

    public class JobAnnouncementRequestViewModel
    {
        public int jobAnnouncementId { get; set; }
    }
}
