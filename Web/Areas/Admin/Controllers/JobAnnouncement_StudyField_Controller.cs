using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Web.Controllers;
using Web.Model;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    public class JobAnnouncement_StudyField_Controller : BaseController
    {
        readonly IJobAnnouncement_StudyField_Service _ja_studyField_service;

        public JobAnnouncement_StudyField_Controller(IJobAnnouncement_StudyField_Service ja_studyField_service)
        {
            _ja_studyField_service = ja_studyField_service;
        }

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
            var ja_sf_list = _ja_studyField_service.GetAllByJobAnnouncementId(jobAnnouncementId).ToList();

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
        public virtual ActionResult Create(JobAnnouncement_StudyField_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_ja_studyField_service.IsDuplicate(model.JobAnnouncementId, model.StudyFieldId))
                {
                    if (_ja_studyField_service.Add(model.JobAnnouncementId, model.StudyFieldId))
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
            var model = _ja_studyField_service.Get(jobAnnouncementId, studyFieldId);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(JobAnnouncement_StudyField_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_ja_studyField_service.Edit(model))
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
        public bool Delete(int jobAnnouncementId, int studyFieldId)
        {
            return _ja_studyField_service.Delete(jobAnnouncementId, studyFieldId);
        }

        [Route("Fill_JA_SF_Combo")]
        public virtual JsonResult Fill_JA_SF_Combo(int jobAnnouncementId)
        {
            var DataList = _ja_studyField_service.GetAllByJobAnnouncementId(jobAnnouncementId)
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
