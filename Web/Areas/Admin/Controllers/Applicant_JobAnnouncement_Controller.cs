using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class Applicant_JobAnnouncement_Controller(IApplicant_JobAnnouncement_Service a_ja_service, IJobAnnouncementService jobAnnouncementService, IApplicantService applicantService) : BaseController
    {
        [Route("ApplicantIndex/{applicantId}")]
        public IActionResult ApplicantIndex(int applicantId)
        {
            var applicantViewModel = applicantService.Get(applicantId);

            if (applicantViewModel == null)
            {
                ShowDangerToast("", "داوطلب یافت نشد");
                return RedirectToAction("Index", "Applicant", new {Area = "Admin"});
            }

            ViewBag.ApplicantId = applicantViewModel.ApplicantId;
            ViewBag.ApplicantFullName = applicantViewModel.FullName;

            return View();
        }

        [Route("JAIndex/{jobAnnouncementId}")]
        public IActionResult JAIndex(int jobAnnouncementId)
        {
            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                ShowDangerToast("", "آگهی یافت نشد.");
                return RedirectToAction("Index", "JobAnnouncement", new { Area = "Admin" });
            }

            ViewBag.JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId;
            ViewBag.JobAnnouncementTitle = jobAnnouncementViewModel.Title;

            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, string filterJobAnnouncementId, 
                                                    string filterApplicantId, string filterInsertDateFrom, string filterInsertDateTo)
        {
            var result = new DataSourceResult()
            {
                Data = a_ja_service.GetAllFiltered(filterJobAnnouncementId, filterApplicantId,
                                                   filterInsertDateFrom, filterInsertDateTo,
                                                   request.Page, request.PageSize, out int totalRecord).ToList(),
                Total = totalRecord // Total number of records
            };

            return Json(result);
        }

        [HttpGet]
        [Route("Create/{jobAnnouncementId}")]
        public IActionResult Create(int jobAnnouncementId)
        {
            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                ShowDangerToast("", "آگهی یافت نشد.");
                return RedirectToAction("Index", "JobAnnouncement", new { Area = "Admin" });
            }

            ViewBag.JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId;
            ViewBag.JobAnnouncementTitle = jobAnnouncementViewModel.Title;

            return View();
        }

        [HttpPost]
        [Route("Create/{jobAnnouncementId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Applicant_JobAnnouncement_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!a_ja_service.IsDuplicate(model.ApplicantId, model.JobAnnouncementId))
                {
                    if (a_ja_service.Add(model))
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
            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                ShowDangerToast("", "آگهی یافت نشد.");
                return RedirectToAction("Index", "JobAnnouncement", new { Area = "Admin" });
            }

            ViewBag.JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId;
            ViewBag.JobAnnouncementTitle = jobAnnouncementViewModel.Title;
                
            var model = a_ja_service.Get(applicantId, jobAnnouncementId);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{jobAnnouncementId}/{applicantId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Applicant_JobAnnouncement_ViewModel model)
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
