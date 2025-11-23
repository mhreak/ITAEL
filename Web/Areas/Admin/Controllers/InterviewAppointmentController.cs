using DbEntities;
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
    public class InterviewAppointmentController(IInterviewAppointmentService interviewAppointmentService, IJobAnnouncementService jobAnnouncementService) : BaseController
    {
        [Route("Index/{jobAnnouncementId}")]
        public IActionResult Index(int jobAnnouncementId)
        {
            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                ShowDangerToast(null, "آگهی یافت نشد.");
            }

            ViewBag.JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId;
            ViewBag.JobAnnouncementTitle = jobAnnouncementViewModel.Title;
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, string filterJobAnnouncementId,
                                            string filterApplicantId, string filterApplicantFirstName, string filterApplicantLastName, 
                                            string filterStatus, string filterInsertDateFrom, string filterInsertDateTo)
        {
            //Paging and Sorting

            int currentPage = request.Page;
            int pageSize = request.PageSize;

            var result = new DataSourceResult()
            {
                Data = interviewAppointmentService.GetAllFiltered(filterJobAnnouncementId, filterApplicantId,filterApplicantFirstName, filterApplicantLastName,
                                                                   filterStatus, filterInsertDateFrom, filterInsertDateTo,
                                                                   currentPage, pageSize, out int totalRecord),
                Total = totalRecord // Total number of records
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
        [Route("Create/{jobAnnouncementId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InterviewAppointmentViewModel model, int jobAnnouncementId)
        {
            if (ModelState.IsValid)
            {
                if (interviewAppointmentService.Add(model) != -1)
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index", new { jobAnnouncementId = jobAnnouncementId });
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

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var model = interviewAppointmentService.Get(id);
            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InterviewAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (interviewAppointmentService.Edit(model))
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
        public bool Delete(int id)
        {
            return interviewAppointmentService.Delete(id);
        }
    }
}
