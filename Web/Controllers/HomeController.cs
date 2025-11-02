using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Web.Model;
using Web.Service.Interface;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        readonly IJobAnnouncementService _jobAnnouncementService;
        readonly IJobAnnouncement_Skill_Service _ja_skill_service;
        readonly IJobAnnouncement_StudyField_Service _ja_studyField_service;
        readonly IJobAnnouncement_JobAnnouncementCategory_Service _ja_category_service;

        public HomeController(
            IJobAnnouncementService jobAnnouncementService,
            IJobAnnouncement_Skill_Service ja_skill_service,
            IJobAnnouncement_StudyField_Service ja_studyField_service,
            IJobAnnouncement_JobAnnouncementCategory_Service ja_category_service)
        {
            _ja_skill_service = ja_skill_service;
            _ja_category_service = ja_category_service;
            _ja_studyField_service = ja_studyField_service;
            _jobAnnouncementService = jobAnnouncementService;
        }

        public IActionResult Index()
        {
            //return View();
            return RedirectToAction("Login", "Account");
        }

        [Route("GetAnnouncements")]
        public IList<JobAnnouncementViewModel> GetAnnouncements(
    string filterJobType, string filterJobTime, string filterCategoryId,
    string filterStudyFieldId, string filterSkillId)
        {
            var list1 = new List<JobAnnouncementViewModel>();
            var list2 = new List<JobAnnouncementViewModel>();
            var list3 = new List<JobAnnouncementViewModel>();
            var list4 = new List<JobAnnouncementViewModel>();

            int totalRecord = 0;
            list1 = _jobAnnouncementService.GetAllFiltered(null, null,
                null, null, null, null, null, null, null, "true", filterJobType, filterJobTime,
                1, int.MaxValue, out totalRecord).ToList();
            //list1.Add(jobAnnouncement);
            if (!String.IsNullOrEmpty(filterCategoryId))
            {
                var ja_jac_list = _ja_category_service.GetAllByJobAnnouncementCategoryId(Convert.ToInt32(filterCategoryId));

                if (ja_jac_list != null && ja_jac_list.Any())
                {
                    list2 = list1.Where(ja => ja_jac_list.Any(x => x.JobAnnouncementId == ja.JobAnnouncementId)).ToList();
                }
                else
                {
                    list2 = new List<JobAnnouncementViewModel>();
                }
            }
            else
            {
                list2 = list1;
            }

            if (!String.IsNullOrEmpty(filterStudyFieldId))
            {
                var ja_studyField_list = _ja_studyField_service.GetAllByStudyFieldId(Convert.ToInt32(filterStudyFieldId));

                if (ja_studyField_list != null && ja_studyField_list.Any())
                {
                    list3 = list2.Where(ja => ja_studyField_list.Any(x => x.JobAnnouncementId == ja.JobAnnouncementId)).ToList();
                }
                else
                {
                    list3 = new List<JobAnnouncementViewModel>();
                }
            }
            else
            {
                list3 = list2;
            }

            if (!String.IsNullOrEmpty(filterSkillId))
            {
                var ja_skill_list = _ja_studyField_service.GetAllByStudyFieldId(Convert.ToInt32(filterSkillId));

                if (ja_skill_list != null && ja_skill_list.Any())
                {
                    list4 = list2.Where(ja => ja_skill_list.Any(x => x.JobAnnouncementId == ja.JobAnnouncementId)).ToList();
                }
                else
                {
                    list4 = new List<JobAnnouncementViewModel>();
                }
            }
            else
            {
                list4 = list3;
            }

            return list4;
        }
    }
}
