using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class CollaboratorViewModel
    {
        [Display(Name = "شناسه")]
        public int CollaboratorId { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string LastName { get; set; }

        [StringLength(11)]
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [Display(Name = "ایمیل")]
        public string? Email { get; set; }

        [StringLength(6)]
        [Display(Name = "کد معرف")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public string ReferralCode { get; set; }

        [Display(Name = "فعال")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Active { get; set; }

        [Display(Name = "فعال")]
        public string? ActiveStr { get; set; }
    }
}
