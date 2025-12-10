using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;

using Web.Model;
using Web.Service.Interface;

namespace Web.Controllers
{
    [AllowAnonymous]
    public class HomeController(
        ISettingService settingService,
        IContactMessageService contactMessageService,
        IJobAnnouncementService jobAnnouncementService,
        IJobAnnouncement_Skill_Service ja_skill_service,
        IJobAnnouncement_StudyField_Service ja_studyField_service,
        IJobAnnouncement_JobAnnouncementCategory_Service ja_category_service)
        : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            var model = new AboutUsSettingViewModel()
                        {
                            Description = settingService.GetValueByKey("AboutUs_Description")
                        };

            return View(model);
        }

        public IActionResult ContactUs()
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
        public IActionResult SubmitContactMessage(ContactMessageViewModel model)
        {
            int contactMessageId = contactMessageService.Add(model);

            if (contactMessageId == -1)
            {
                TempData["ErrorMessage"] = "درخواست شما ثبت نشد لطفا دوباره تلاش کنید.";
                return View(model);
            }

            return RedirectToAction("ContactUs");
        }

        [HttpGet]
        public IList<JobAnnouncementViewModel> GetAnnouncements(
    string filterJobType, string filterJobTime, string filterCategoryId,
    string filterStudyFieldId, string filterSkillId)
        {
            var list1 = new List<JobAnnouncementViewModel>();
            var list2 = new List<JobAnnouncementViewModel>();
            var list3 = new List<JobAnnouncementViewModel>();
            var list4 = new List<JobAnnouncementViewModel>();

            int totalRecord = 0;
            list1 = jobAnnouncementService.GetAllFiltered(null, null,
                null, null, null, null, null, null, null, "true", filterJobType, filterJobTime,
                1, int.MaxValue, out totalRecord).ToList();
            //list1.Add(jobAnnouncement);
            if (!String.IsNullOrEmpty(filterCategoryId))
            {
                var ja_jac_list = ja_category_service.GetAllByJobAnnouncementCategoryId(Convert.ToInt32(filterCategoryId));

                if (ja_jac_list != null && ja_jac_list.Any())
                {
                    list2 = list1.Where(ja => ja_jac_list.Any(x => x.JobAnnouncementId == ja.JobAnnouncementId)).ToList();
                }
                else
                {
                    list2 = new List<JobAnnouncementViewModel>();
                }
            }
            else
            {
                list2 = list1;
            }

            if (!String.IsNullOrEmpty(filterStudyFieldId))
            {
                var ja_studyField_list = ja_studyField_service.GetAllByStudyFieldId(Convert.ToInt32(filterStudyFieldId));

                if (ja_studyField_list != null && ja_studyField_list.Any())
                {
                    list3 = list2.Where(ja => ja_studyField_list.Any(x => x.JobAnnouncementId == ja.JobAnnouncementId)).ToList();
                }
                else
                {
                    list3 = new List<JobAnnouncementViewModel>();
                }
            }
            else
            {
                list3 = list2;
            }

            if (!String.IsNullOrEmpty(filterSkillId))
            {
                var ja_skill_list = ja_studyField_service.GetAllByStudyFieldId(Convert.ToInt32(filterSkillId));

                if (ja_skill_list != null && ja_skill_list.Any())
                {
                    list4 = list2.Where(ja => ja_skill_list.Any(x => x.JobAnnouncementId == ja.JobAnnouncementId)).ToList();
                }
                else
                {
                    list4 = new List<JobAnnouncementViewModel>();
                }
            }
            else
            {
                list4 = list3;
            }

            return list4;
        }
    }
}
