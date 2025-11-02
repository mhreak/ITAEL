using Microsoft.AspNetCore.Mvc;
using Web.Model;
using Web.Service.Interface;

namespace Web.Controllers
{
    public class JobAnnouncementController : Controller
    {
        private readonly IJobAnnouncementService _jobAnnouncementService;
        private readonly IJobAnnouncement_StudyField_Service _studyFieldService;
        private readonly IJobAnnouncement_Skill_Service _skillService;

        public JobAnnouncementController(IJobAnnouncementService jobAnnouncementService, IJobAnnouncement_StudyField_Service studyFieldService, IJobAnnouncement_Skill_Service skillService)
        {
            _jobAnnouncementService = jobAnnouncementService;
            _studyFieldService = studyFieldService;
            _skillService = skillService;
        }
        public IActionResult Details(int id)
        {
            JobAnnouncementViewModel jobAnnouncement = _jobAnnouncementService.Get(id);
            ViewBag.JonAnnouncement_SkillList = _skillService.GetAllByJobAnnouncementId(id);
            ViewBag.JonAnnouncement_StudyFieldList = _studyFieldService.GetAllByJobAnnouncementId(id);
            return View(jobAnnouncement);
        }

    }
}
