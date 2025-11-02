using System.ComponentModel.DataAnnotations;

namespace Web.Model.Identity
{
    public class CreateUserViewModel
    {
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        public string Name { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string UserName { get; set; }

        [Phone]
        [Display(Name = "موبایل")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "نقش")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        public int RoleId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        [StringLength(20, ErrorMessage = "رمز عبور باید بین 6 تا 20 کاراکتر باشد", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "واردکردن این فیلد الزامی است")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن با هم مطابقت ندارند")]
        [StringLength(20, ErrorMessage = "رمز عبور باید بین 6 تا 20 کاراکتر باشد", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}