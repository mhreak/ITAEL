using System.Collections.Generic;

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
    public class SystemSMSController(ISystemSMSService systemSMSService, IApplicantService applicantService, ISMSPatternService smsPatternService, IFarazSMSService farazSMSService) : BaseController
    {

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Grid_Data_Read")]
        public IActionResult Grid_Data_Read([DataSourceRequest] DataSourceRequest request, string filterMobile, string filterSMSType,
                                            string filterSendDateFrom, string filterSendDateTo)
        {
            //Paging and Sorting

            int currentPage = request.Page;
            int pageSize = request.PageSize;

            var result = new DataSourceResult()
            {
                Data = systemSMSService.GetAllFiltered(filterMobile, filterSMSType, filterSendDateFrom, filterSendDateTo,
                                                       currentPage, pageSize, out int totalRecord),
                Total = totalRecord // Total number of records
            };

            return Json(result);
        }

        [HttpGet]
        [Route("SendSMS")]
        public IActionResult SendSMS()
        {
            return View();
        }

        [HttpPost]
        [Route("SendSMS")]
        [ValidateAntiForgeryToken]
        public IActionResult SendSMS(SMSViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mobileList = new List<string>();
                foreach (var applicantId in model.ApplicantIdList)
                {
                    var applicantViewModel = applicantService.Get(applicantId);
                    mobileList.Add(applicantViewModel.Mobile);
                }

                var smsPattern = smsPatternService.GetByTemplateType(model.SystemSMSViewModel.SMSType);

                var isSend = farazSMSService.SendSMS(mobileList, smsPattern.TemplateText);

                if (isSend)
                {
                    foreach (var mobile in mobileList)
                    {
                        model.SystemSMSViewModel.Mobile = mobile;
                        systemSMSService.Add(model.SystemSMSViewModel);
                    }

                    return RedirectToAction("Index");
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
    }

    public class SMSViewModel
    {
        public List<int> ApplicantIdList { get; set; }

        public SystemSMSViewModel SystemSMSViewModel { get; set; }
    }
}
