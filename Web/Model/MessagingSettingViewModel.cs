using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class MessagingSettingViewModel
    {
        [Display(Name = "ارسال پیامک هنگام ثبت نام در سامانه")]
        public bool SendSMSOnSuccessfulRegisterInWebsite { get; set; }

        [Display(Name = "ارسال پیامک هنگام پرداخت موفق")]
        public bool SendSMSOnSuccessfulPayment { get; set; }

        [Display(Name = "ارسال پیامک هنگام ثبت نام موفق برای آگهی")]
        public bool SendSMSOnSuccessfulAnnouncementApply { get; set; }
    }
}
