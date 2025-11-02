using System.ComponentModel.DataAnnotations;
using System;

namespace Web.Model
{
    public class ReferralCodeViewModel
    {
        [Display(Name = "شناسه")]
        public int ReferralCodeId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز ۵۰ کاراکتر است")]
        public string ReferralCodeName { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(30, ErrorMessage = "حداکثر طول مجاز ۳۰ کاراکتر است")]
        public string RefCode { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Active { get; set; }

        [Display(Name = "وضعیت")]
        public string ActiveStr { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }
    }
}
