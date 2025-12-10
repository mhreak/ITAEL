using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ContactUsSettingViewModel
    {
        [Display(Name = "شماره ثابت 1")]
        public string? PrimaryLandline { get; set; }

        [Display(Name = "شماره ثابت 2")]
        public string? SecondaryLandline { get; set; }

        [Display(Name = "شماره تلفن همراه 1")]
        public string? PrimaryMobile { get; set; }

        [Display(Name = "شماره تلفن همراه 2")]
        public string? SecondaryMobile { get; set; }

        [Display(Name = "آی دی تلگرام")]
        public string? TelegramID { get; set; }

        [Display(Name = "آی دی اینستاگرام")]
        public string? InstagramID { get; set; }

        [Display(Name = "شماره واتساپ")]
        public string? WhatsAppNumber { get; set; }

        [Display(Name = "ایمیل")]
        public string? Email { get; set; }

        [Display(Name = "آدرس")]
        public string? Address { get; set; }
    }
}
