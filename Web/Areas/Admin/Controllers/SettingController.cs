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
    public class SettingController(ISettingService settingService) : BaseController
    {
        [Route("SMSSetting")]
        public IActionResult SMSSetting()
        {
            var model = new SMSSettingViewModel()
            {
                SMSPanelUsername = settingService.GetValueByKey("SMSPanelUsername"),
                SMSPanelPassword = settingService.GetValueByKey("SMSPanelPassword"),
                SMSSenderNumber = settingService.GetValueByKey("SMSSenderNumber"),
                ServiceSMSSenderNumber = settingService.GetValueByKey("SMSSenderNumber")
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
                settingService.SetSettingValue("SMSPanelUsername", model.SMSPanelUsername);
                settingService.SetSettingValue("SMSPanelPassword", model.SMSPanelPassword);
                settingService.SetSettingValue("SMSSenderNumber", model.SMSSenderNumber);
                settingService.SetSettingValue("SMSSenderNumber", model.ServiceSMSSenderNumber);

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
                SendSMSOnSuccessfulRegisterInWebsite = Convert.ToBoolean(settingService.GetValueByKey("SendSMSOnSuccessfulRegisterInWebsite")),
                SendSMSOnSuccessfulPayment = Convert.ToBoolean(settingService.GetValueByKey("SendSMSOnSuccessfulPayment")),
                SendSMSOnSuccessfulAnnouncementApply = Convert.ToBoolean(settingService.GetValueByKey("SendSMSOnSuccessfulAnnouncementApply"))
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
                settingService.SetSettingValue("SendSMSOnSuccessfulRegisterInWebsite", model.SendSMSOnSuccessfulRegisterInWebsite.ToString());
                settingService.SetSettingValue("SendSMSOnSuccessfulPayment", model.SendSMSOnSuccessfulPayment.ToString());
                settingService.SetSettingValue("SendSMSOnSuccessfulAnnouncementApply", model.SendSMSOnSuccessfulAnnouncementApply.ToString());

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
