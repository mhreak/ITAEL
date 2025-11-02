using DbEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Model
{
    public class CollaboratorViewModel
    {
        [Required]
        [Display(Name = "شناسه")]
        public int CollaboratorId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required]
        [StringLength(11)]
        [Display(Name = "شماره موبایل")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required]
        [StringLength(6)]
        [Display(Name = "کد معرف")]
        public string ReferralCode { get; set; }

        [Required]
        [Display(Name = "شناسه سازمان")]
        public int CompanyId { get; set; }

        [Display(Name = "نام سازمان")]
        public string? CompanyName { get; set; }

        [Required]
        [Display(Name = "فعال")]
        public bool Active { get; set; }

        public string? ActiveStr { get; set; }
    }
}
