using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Model.Identity
{
    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Name { get; set; }

        [Display(Name = "نام کاربری (فقط انگلیسی)")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string Username { get; set; }

        [Phone]
        [Display(Name = "موبایل")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "نفش")]
        public int RoleId { get; set; }

        [Display(Name = "نقش های کاربری")]
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
