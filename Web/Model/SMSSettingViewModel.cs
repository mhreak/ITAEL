using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class SMSSettingViewModel
    {
        [Display(Name = "نام کاربری پنل پیامک")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        public string SMSPanelUsername { get; set; }

        [Display(Name = "رمز عبور پنل پیامک")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        public string SMSPanelPassword { get; set; }

        [Display(Name = "شماره ارسال کننده پیامک")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        public string SMSSenderNumber { get; set; }


        [Display(Name = "شماره ارسال کننده پیامک خدماتی")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        public string ServiceSMSSenderNumber { get; set; }
    }
}
