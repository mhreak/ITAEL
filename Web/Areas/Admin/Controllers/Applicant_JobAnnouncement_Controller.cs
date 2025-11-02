using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.Controllers;
using Web.Model;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class Applicant_JobAnnouncement_Controller(IApplicant_JobAnnouncement_Service a_ja_service) : BaseController
    {
        [Route("ApplicantIndex/{applicantId}")]
        public IActionResult ApplicantIndex(int applicantId)
        {
            ViewBag.ApplicantId = applicantId;
            return View();
        }

        [Route("ApplicantIndex_Grid_Data_Read")]
        public IActionResult ApplicantIndex_Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            int applicantId)
        {
            var ja_sf_list = a_ja_service.GetAllByApplicantId(applicantId).ToList();

            var result = new DataSourceResult()
            {
                Data = ja_sf_list,
                Total = ja_sf_list.Count // Total number of records
            };

            return Json(result);
        }

        [Route("JAIndex/{jobAnnouncementId}")]
        public IActionResult JAIndex(int jobAnnouncementId)
        {
            ViewBag.JobAnnouncementId = jobAnnouncementId;
            return View();
        }

        [Route("JAIndex_Grid_Data_Read")]
        public IActionResult JAIndex_Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
            int jobAnnouncementId)
        {
            var ja_sf_list = a_ja_service.GetAllByJobAnnouncementId(jobAnnouncementId).ToList();

            var result = new DataSourceResult()
            {
                Data = ja_sf_list,
                Total = ja_sf_list.Count // Total number of records
            };

            return Json(result);
        }

        [HttpGet]
        [Route("Create/{jobAnnouncementId}")]
        public IActionResult Create(int jobAnnouncementId)
        {
            ViewBag.JobAnnouncementId = jobAnnouncementId;

            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(Applicant_JobAnnouncement_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!a_ja_service.IsDuplicate(model.ApplicantId, model.JobAnnouncementId))
                {
                    if (a_ja_service.Add(model.ApplicantId, model.JobAnnouncementId))
                    {
                        ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                        return RedirectToAction("JAIndex", new { jobAnnouncementId = model.JobAnnouncementId });
                    }
                    else
                    {
                        ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                    }
                }
                else
                {
                    ShowDangerToast(null, "این داوطلب قبلا برای این آگهی درخواست داده است");
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
        [Route("Edit/{jobAnnouncementId}/{applicantId}")]
        public IActionResult Edit(int jobAnnouncementId, int applicantId)
        {
            var model = a_ja_service.Get(applicantId, jobAnnouncementId);
            return View(model);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(Applicant_JobAnnouncement_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (a_ja_service.Edit(model))
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("JAIndex", new { jobAnnouncementId = model.JobAnnouncementId });
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
        public bool Delete(int applicantId, int jobAnnouncementId)
        {
            return a_ja_service.Delete(applicantId, jobAnnouncementId);
        }
    }
}
