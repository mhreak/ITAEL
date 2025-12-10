using Web.Model;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.Service.Identity.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Applicant.Controllers
{
    [Area("Applicant")]
    [Route("Applicant/[controller]")]
    [Authorize(Roles = "Applicant,Admin")]
    public class DashboardController(IJobAnnouncementService jobAnnouncementService,
                                     IExamResourceService examResourceService,
                                     IJobAnnouncement_ExamResource_Service ja_ExamResource_Service,
                                     IJobAnnouncement_StudyField_Service jobAnnouncementStudyFieldService,
                                     IApplicationUserManagerService applicationUserManagerService,
                                     IApplicantService applicantService,
                                     IApplicant_JobAnnouncement_Service applicantJobAnnouncementService) : BaseController
    {
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            var user = applicationUserManagerService.GetCurrentUser();

            if (user == null || !user.ApplicantId.HasValue)
            {
                TempData["ErrorMessage"] = "لطفا ورود بزنید.";
                return RedirectToAction("Login", "Account", new { Area = "Applicant" });
            }

            var applicantViewModel = applicantService.Get(user.ApplicantId.Value);

            var applicantJobAnnouncementViewModelList = applicantJobAnnouncementService.GetAllByApplicantId(applicantViewModel.ApplicantId);

            List<JobAnnouncementViewModel> jobAnnouncementViewModelList = [];
            foreach (var applicantJobAnnouncementViewModel in applicantJobAnnouncementViewModelList)
            {
                var jobAnnouncementViewModel = jobAnnouncementService.Get(applicantJobAnnouncementViewModel.JobAnnouncementId);
                jobAnnouncementViewModelList.Add(jobAnnouncementViewModel);
            }

            var applicantDashboardViewModelList = new List<ApplicantDashboardViewModel>();
            foreach (var jobAnnouncementViewModel in jobAnnouncementViewModelList)
            {
                var applicantDashboardViewModel = new ApplicantDashboardViewModel();
                var jobAnnouncementStudyFieldViewModelList = jobAnnouncementStudyFieldService.GetAllByJobAnnouncementId(jobAnnouncementViewModel.JobAnnouncementId);

                applicantDashboardViewModel.JobAnnouncementViewModel = jobAnnouncementViewModel;
                applicantDashboardViewModel.JobAnnouncementStudyFieldViewModelList = (List<JobAnnouncement_StudyField_ViewModel>)jobAnnouncementStudyFieldViewModelList;

                var ja_ExamResource_ViewModel_List = ja_ExamResource_Service.GetAllByJobAnnouncementId(jobAnnouncementViewModel.JobAnnouncementId);

                if (ja_ExamResource_ViewModel_List == null || ja_ExamResource_ViewModel_List.Count == 0)
                {
                    applicantDashboardViewModel.HasExamResource = false;
                }
                else
                {
                    applicantDashboardViewModel.HasExamResource = true;
                }

                applicantDashboardViewModelList.Add(applicantDashboardViewModel);
            }


            return View(applicantDashboardViewModelList);
        }
    }

    public class ApplicantDashboardViewModel
    {
        public bool HasExamResource { get; set; }
        public JobAnnouncementViewModel JobAnnouncementViewModel { get; set; }

        public List<JobAnnouncement_StudyField_ViewModel> JobAnnouncementStudyFieldViewModelList { get; set; }


    }
}
