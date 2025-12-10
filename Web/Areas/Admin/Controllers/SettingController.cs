using System;
using Web.Model;
using System.Linq;
using Web.Controllers;
using Web.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult SMSSetting(SMSSettingViewModel model)
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
        public IActionResult MessagingSetting(MessagingSettingViewModel model)
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

        [Route("AboutUsSetting")]
        public IActionResult AboutUsSetting()
        {
            var model = new AboutUsSettingViewModel()
            {
                Description = settingService.GetValueByKey("AboutUs_Description")
            };

            return View(model);
        }

        [HttpPost]
        [Route("AboutUsSetting")]
        [ValidateAntiForgeryToken]
        public IActionResult AboutUsSetting(AboutUsSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                settingService.SetSettingValue("AboutUs_Description", model.Description);

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

        [Route("ContactUsSetting")]
        public IActionResult ContactUsSetting()
        {
            var model = new ContactUsSettingViewModel()
                        {
                            PrimaryLandline = settingService.GetValueByKey("ContactUs_PrimaryLandline"),
                            SecondaryLandline = settingService.GetValueByKey("ContactUs_SecondaryLandline"),
                            PrimaryMobile = settingService.GetValueByKey("ContactUs_PrimaryMobileNumber"),
                            SecondaryMobile = settingService.GetValueByKey("ContactUs_SecondaryMobileNumber"),
                            TelegramID = settingService.GetValueByKey("ContactUs_TelegramID"),
                            InstagramID = settingService.GetValueByKey("ContactUs_InstagramID"),
                            WhatsAppNumber = settingService.GetValueByKey("ContactUs_WhatsAppNumber"),
                            Email = settingService.GetValueByKey("ContactUs_Email"),
                            Address = settingService.GetValueByKey("ContactUs_Address"),
                        };

            return View(model);
        }

        [HttpPost]
        [Route("ContactUsSetting")]
        [ValidateAntiForgeryToken]
        public IActionResult ContactUsSetting(ContactUsSettingViewModel model)
        {
            if (ModelState.IsValid)
            {
                settingService.SetSettingValue("ContactUs_PrimaryLandline", model.PrimaryLandline);
                settingService.SetSettingValue("ContactUs_SecondaryLandline", model.SecondaryLandline);
                settingService.SetSettingValue("ContactUs_PrimaryMobileNumber", model.PrimaryMobile);
                settingService.SetSettingValue("ContactUs_SecondaryMobileNumber", model.SecondaryMobile);
                settingService.SetSettingValue("ContactUs_TelegramID", model.TelegramID);
                settingService.SetSettingValue("ContactUs_InstagramID", model.InstagramID);
                settingService.SetSettingValue("ContactUs_WhatsAppNumber", model.WhatsAppNumber);
                settingService.SetSettingValue("ContactUs_Email", model.Email);
                settingService.SetSettingValue("ContactUs_Address", model.Address);

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
