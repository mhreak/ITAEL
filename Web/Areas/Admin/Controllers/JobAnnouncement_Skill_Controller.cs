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
    public class JobAnnouncement_Skill_Controller(IJobAnnouncement_Skill_Service ja_skill_service) : BaseController
    {
        [Route("Index/{jobAnnouncementId}")]
        public IActionResult Index(int jobAnnouncementId)
        {
            ViewBag.jobAnnouncementId = jobAnnouncementId;
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            int jobAnnouncementId)
        {
            var ja_sf_list = ja_skill_service.GetAllByJobAnnouncementId(jobAnnouncementId).ToList();

            var result = new DataSourceResult()
            {
                Data = ja_sf_list,
                Total = ja_sf_list.Count // Total number of records
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
        public virtual ActionResult Create(JobAnnouncement_Skill_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!ja_skill_service.IsDuplicate(model.JobAnnouncementId, model.SkillId))
                {
                    if (ja_skill_service.Add(model.JobAnnouncementId, model.SkillId))
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
        [Route("Edit/{jobAnnouncementId}/{studyFieldId}")]
        public IActionResult Edit(int jobAnnouncementId, int studyFieldId)
        {
            var model = ja_skill_service.Get(jobAnnouncementId, studyFieldId);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(JobAnnouncement_Skill_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ja_skill_service.Edit(model))
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
        public bool Delete(int jobAnnouncementId, int skillId)
        {
            return ja_skill_service.Delete(jobAnnouncementId, skillId);
        }

        [Route("Fill_JA_Skill_Combo")]
        public virtual JsonResult Fill_JA_Skill_Combo(int jobAnnouncementId)
        {
            var DataList = ja_skill_service.GetAllByJobAnnouncementId(jobAnnouncementId)
                .Select(x =>
                new SelectListItem
                {
                    Text = x.SkillName,
                    Value = x.SkillId.ToString()
                });
            return Json(DataList);
        }
    }
}
