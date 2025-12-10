using Web.Model;
using System.Linq;
using Kendo.Mvc.UI;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Areas.Admin.Controllers
{
    public class ExamResourceOrderItemController(IExamResourceOrderItemService examResourceOrderItemService, IExamResourceOrderService examResourceOrderService) : BaseController
    {
        [Route("Index/{examResourceOrderId}")]
        public IActionResult Index(int examResourceOrderId)
        {
            var examResourceOrderViewModel = examResourceOrderService.Get(examResourceOrderId);

            if (examResourceOrderViewModel == null)
            {
                ShowDangerToast(null, "سفارش منبع آزمون یافت نشد.");
            }

            ViewBag.ExamResourceOrderId = examResourceOrderViewModel.ExamResourceOrderId;
            ViewBag.ApplicantId = examResourceOrderViewModel.ApplicantId;
            ViewBag.ApplicantFullName = examResourceOrderViewModel.ApplicantFullName;
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request,
                                            string filterExamResourceId, string filterExamResourceOrderId,
                                            string filterInsertDateFrom, string filterInsertDateTo)
        {
            var result = new DataSourceResult()
            {
                Data = examResourceOrderItemService.GetAllFiltered(filterExamResourceId, filterExamResourceOrderId,
                                                                   filterInsertDateFrom, filterInsertDateTo,
                                                                   request.Page, request.PageSize, out int totalRecord),
                Total = totalRecord // Total number of records
            };

            return Json(result);
        }

        [HttpGet]
        [Route("Create/{examResourceOrderId}")]
        public IActionResult Create(int examResourceOrderId)
        {
            var examResourceOrderViewModel = examResourceOrderService.Get(examResourceOrderId);

            if (examResourceOrderViewModel == null)
            {
                ShowDangerToast(null, "سفارش منبع آزمون یافت نشد.");
            }

            ViewBag.ExamResourceOrderId = examResourceOrderViewModel.ExamResourceOrderId;
            ViewBag.ApplicantId = examResourceOrderViewModel.ApplicantId;
            ViewBag.ApplicantFullName = examResourceOrderViewModel.ApplicantFullName;

            return View();
        }

        [HttpPost]
        [Route("Create/{examResourceOrderId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExamResourceOrderItemViewModel model, int examResourceOrderId)
        {
            if (ModelState.IsValid)
            {
                int examResourceOrderItemId = examResourceOrderItemService.Add(model);
                if (examResourceOrderItemId != -1)
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index", new { examResourceOrderId = examResourceOrderId });
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
        [Route("Edit/{examResourceOrderItemId}")]
        public IActionResult Edit(int examResourceOrderItemId)
        {
            var examResourceOrderItemViewModel = examResourceOrderItemService.Get(examResourceOrderItemId);

            if (examResourceOrderItemViewModel == null)
            {
                ShowDangerToast("", "آیتم سفارش منبع آزمون یافت نشد.");
            }

            var examResourceOrderViewModel = examResourceOrderService.Get(examResourceOrderItemViewModel.ExamResourceId);

            if (examResourceOrderViewModel == null)
            {
                ShowDangerToast("", "سفارش منبع آزمون یافت نشد.");
            }

            ViewBag.ExamResourceOrderItemId = examResourceOrderItemViewModel.ExamResourceOrderItemId;
            ViewBag.ExamResourceOrderId = examResourceOrderItemViewModel.ExamResourceOrderId;
            ViewBag.ApplicantId = examResourceOrderViewModel.ApplicantId;
            ViewBag.ApplicantFullName = examResourceOrderViewModel.ApplicantFullName;

            return View(examResourceOrderItemViewModel);
        }

        [HttpPost]
        [Route("Edit/{examResourceOrderItemId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExamResourceOrderItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (examResourceOrderItemService.Edit(model))
                {
                    ShowSuccessToast("عملیات انجام شد", "اطلاعات با موفقیت ذخیره شد");

                    return RedirectToAction("Index", new { examResourceOrderId = model.ExamResourceOrderId });
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
        public bool Delete(int examResourceOrderItemId)
        {
            return examResourceOrderItemService.Delete(examResourceOrderItemId);
        }

        [Route("Fill_ExamResourceOrderItem_Combo")]
        public virtual JsonResult Fill_ExamResourceOrderItem_Combo(int examResourceOrderId)
        {
            var DataList = examResourceOrderItemService.GetAllByExamResourceId(examResourceOrderId)
                                                       .Select(x =>
                                                                   new SelectListItem
                                                                   {
                                                                       Text = x.ResourceName,
                                                                       Value = x.ExamResourceOrderItemId.ToString()
                                                                   });
            return Json(DataList);
        }
    }
}
