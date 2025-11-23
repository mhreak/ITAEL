using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class JobAnnouncement_ExamResource_Controller(IJobAnnouncement_ExamResource_Service ja_ExamResource_Service, IJobAnnouncementService jobAnnouncementService) : BaseController
    {
        [Route("Index/{jobAnnouncementId}")]
        public IActionResult Index(int jobAnnouncementId)
        {
            var jobAnnouncementViewModel = jobAnnouncementService.Get(jobAnnouncementId);

            if (jobAnnouncementViewModel == null)
            {
                ShowDangerToast("", "آگهی یافت نشد.");
            }

            ViewBag.JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId;
            ViewBag.JobAnnouncementTitle = jobAnnouncementViewModel.Title;

            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, string filterJobAnnouncementId,
                                            string filterExamResourceId, string filterInsertDateFrom, string filterInsertDateTo)
        {
            int currentPage = request.Page;
            int pageSize = request.PageSize;

            var result = new DataSourceResult()
            {
                Data = ja_ExamResource_Service.GetAllFiltered(filterJobAnnouncementId, filterExamResourceId,
                                                              filterInsertDateFrom, filterInsertDateTo,
                                                              currentPage, pageSize, out int totalRecord),
                Total = totalRecord// Total number of records
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
            }

            ViewBag.JobAnnouncementId = jobAnnouncementViewModel.JobAnnouncementId;
            ViewBag.JobAnnouncementTitle = jobAnnouncementViewModel.Title;

            return View();
        }

        [HttpPost]
        [Route("Create/{jobAnnouncementId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(JobAnnouncement_ExamResource_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!ja_ExamResource_Service.IsDuplicate(model.JobAnnouncementId, model.ExamResourceId))
                {
                    if (ja_ExamResource_Service.Add(model.JobAnnouncementId, model.ExamResourceId))
                    {
                        ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                        return RedirectToAction("Index", new { jobAnnouncementId = model.JobAnnouncementId });
                    }
                    else
                    {
                        ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                        return RedirectToAction("Index", new { jobAnnouncementId = model.JobAnnouncementId });
                    }
                }
                else
                {
                    ShowDangerToast(null, "یک منبع آزمون دیگر برای این آگهی وجود دارد");
                    return RedirectToAction("Index", new { jobAnnouncementId = model.JobAnnouncementId });
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
        [Route("Edit/{jobAnnouncementId}/{examResourceId}")]
        public IActionResult Edit(int jobAnnouncementId, int examResourceId)
        {
            var jobAnnouncementExamResourceViewModel = ja_ExamResource_Service.Get(jobAnnouncementId, examResourceId);

            if (jobAnnouncementExamResourceViewModel == null)
            {
                ShowDangerToast("", "آگهی یافت نشد.");
            }


            ViewBag.JobAnnouncementTitle = jobAnnouncementExamResourceViewModel.JobAnnouncementTitle;
            return View(jobAnnouncementExamResourceViewModel);
        }

        [HttpPost]
        [Route("Edit/{jobAnnouncementId}/{examResourceId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JobAnnouncement_ExamResource_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ja_ExamResource_Service.Edit(model))
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index", new { jobAnnouncementId = model.JobAnnouncementId });
                }
                else
                {
                    ShowDangerToast(null, "خطا هنگام ذخیره اطلاعات");
                    return RedirectToAction("Index", new { jobAnnouncementId = model.JobAnnouncementId });
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
        public bool Delete(int jobAnnouncementId, int examResourceId)
        {
            return ja_ExamResource_Service.Delete(jobAnnouncementId, examResourceId);
        }

        [Route("Fill_Ja_ExamResource_Combo")]
        public virtual JsonResult Fill_Ja_ExamResource_Combo(int jobAnnouncementId)
        {
            var DataList = ja_ExamResource_Service.GetAllByJobAnnouncementId(jobAnnouncementId)
                                                  .Select(x =>
                                                              new SelectListItem
                                                              {
                                                                  Text = x.ResourceName,
                                                                  Value = x.ExamResourceId.ToString()
                                                              });
            return Json(DataList);
        }
    }
}
