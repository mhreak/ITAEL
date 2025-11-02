using Microsoft.AspNetCore.Mvc;
using Web.Model;
using Web.Service.Interface;

namespace Web.Controllers
{
    public class JobAnnouncementController(IJobAnnouncementService jobAnnouncementService, 
                                           IJobAnnouncement_StudyField_Service studyFieldService, 
                                           IJobAnnouncement_Skill_Service skillService)
        : Controller
    {
        public IActionResult Details(int id)
        {
            JobAnnouncementViewModel jobAnnouncement = jobAnnouncementService.Get(id);
            ViewBag.JonAnnouncement_SkillList = skillService.GetAllByJobAnnouncementId(id);
            ViewBag.JonAnnouncement_StudyFieldList = studyFieldService.GetAllByJobAnnouncementId(id);
            return View(jobAnnouncement);
        }

    }
}
