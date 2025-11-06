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
    public class InterviewAppointmentController(IInterviewAppointmentService interviewAppointmentService) : BaseController
    {
        [Route("Index/{jobAnnouncementId}")]
        public IActionResult Index(int jobAnnouncementId)
        {
            ViewBag.JobAnnouncementId = jobAnnouncementId;
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, string filterJobAnnouncementId,
                                            string filterApplicantId, string filterStatus, string filterInsertDateFrom, string filterInsertDateTo)
        {
            //Paging and Sorting

            int currentPage = request.Page;
            int pageSize = request.PageSize;

            var result = new DataSourceResult()
            {
                Data = interviewAppointmentService.GetAllFiltered(filterJobAnnouncementId,  filterApplicantId,
                                                                   filterStatus,  filterInsertDateFrom, filterInsertDateTo, 
                                                                   currentPage, pageSize, out int totalRecord),
                Total = totalRecord // Total number of records
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
        public virtual ActionResult Create(InterviewAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (interviewAppointmentService.Add(model) != -1)
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index");
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
        public virtual ActionResult Edit(InterviewAppointmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (interviewAppointmentService.Edit(model))
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index");
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
