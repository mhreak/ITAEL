using System.ComponentModel.DataAnnotations;
using System;

namespace Web.Model
{
    public class CompanyViewModel
    {
        [Display(Name = "شناسه")]
        public int CompanyId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(100, ErrorMessage = "حداکثر طول مجاز ۱۰۰ کاراکتر است")]
        public string CompanyName { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }

        [Display(Name = "تعداد آگهی ها")]
        public int JobAnnouncementCount { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
