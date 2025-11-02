using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Web.Controllers;
using Web.Model;
using Web.Service.Interface;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    [Authorize(Roles = "Manager,Admin")]
    public class SettingController : BaseController
    {
        readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [Route("SMSSetting")]
        public IActionResult SMSSetting()
        {
            var model = new SMSSettingViewModel()
            {
                SMSPanelUsername = _settingService.GetValueByKey("SMSPanelUsername"),
                SMSPanelPassword = _settingService.GetValueByKey("SMSPanelPassword"),
                SMSSenderNumber = _settingService.GetValueByKey("SMSSenderNumber"),
                ServiceSMSSenderNumber = _settingService.GetValueByKey("SMSSenderNumber")
            };

            return View(model);
        }

        [HttpPost]
        [Route("SMSSetting")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SMSSetting(SMSSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                _settingService.SetSettingValue("SMSPanelUsername", model.SMSPanelUsername);
                _settingService.SetSettingValue("SMSPanelPassword", model.SMSPanelPassword);
                _settingService.SetSettingValue("SMSSenderNumber", model.SMSSenderNumber);
                _settingService.SetSettingValue("SMSSenderNumber", model.ServiceSMSSenderNumber);

                ShowSuccessToast(null, "تنظیمات ذخیره شد");
                return View();
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowSuccessToast(null, "اطلاعات واردشده دارای خطا می‌باشد");
            }

            ShowDangerToast(null, "هنگام ذخیره اطلاعات خطایی رخ داد");
            return View(model);
        }

        [Route("MessagingSetting")]
        public IActionResult MessagingSetting()
        {
            var model = new MessagingSettingViewModel()
            {
                SendSMSOnSuccessfulRegisterInWebsite = Convert.ToBoolean(_settingService.GetValueByKey("SendSMSOnSuccessfulRegisterInWebsite")),
                SendSMSOnSuccessfulPayment = Convert.ToBoolean(_settingService.GetValueByKey("SendSMSOnSuccessfulPayment")),
                SendSMSOnSuccessfulAnnouncementApply = Convert.ToBoolean(_settingService.GetValueByKey("SendSMSOnSuccessfulAnnouncementApply"))
            };

            return View(model);
        }

        [HttpPost]
        [Route("MessagingSetting")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult MessagingSetting(MessagingSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                _settingService.SetSettingValue("SendSMSOnSuccessfulRegisterInWebsite", model.SendSMSOnSuccessfulRegisterInWebsite.ToString());
                _settingService.SetSettingValue("SendSMSOnSuccessfulPayment", model.SendSMSOnSuccessfulPayment.ToString());
                _settingService.SetSettingValue("SendSMSOnSuccessfulAnnouncementApply", model.SendSMSOnSuccessfulAnnouncementApply.ToString());

                ShowSuccessToast(null, "تنظیمات ذخیره شد");
                return View();
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                ShowSuccessToast(null, "اطلاعات واردشده دارای خطا می‌باشد");
            }

            ShowDangerToast(null, "هنگام ذخیره اطلاعات خطایی رخ داد");
            return View(model);
        }
    }
}
