using System.ComponentModel.DataAnnotations;

namespace Web.Model.Identity
{
    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        [MinLength(6, ErrorMessage = "این فیلد باید حداقل 6 کاراکتر باشد")]
        [MaxLength(15, ErrorMessage = "این فیلد باید حداکثر 15 کاراکتر باشد")]
        [StringLength(15, ErrorMessage = "کلمه عبور باید حداقل 6 و حداکثر 15 کاراکتر باشد", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        [MinLength(6, ErrorMessage = "این فیلد باید حداقل 6 کاراکتر باشد")]
        [MaxLength(15, ErrorMessage = "این فیلد باید حداکثر 15 کاراکتر باشد")]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار کلمه عبور با هم مطابقت ندارند")]
        [StringLength(15, ErrorMessage = "تکرار کلمه عبور باید حداقل 6 و حداکثر 15 کاراکتر باشد", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }


        [Display(Name = "کد یکبار مصرف")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        [StringLength(5, ErrorMessage = "کد یکبار مصرف باید 5 شماره باشد")]
        public string OTP { get; set; }
    }
}
