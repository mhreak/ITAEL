using System.ComponentModel.DataAnnotations;

namespace Web.Model.Identity
{
    public class ConfirmMobileNumberViewModel
    {
        [Display(Name = "کد یکبار مصرف")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        public string OTP { get; set; }
    }
}
