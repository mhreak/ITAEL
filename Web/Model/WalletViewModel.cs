using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class WalletViewModel
    {
        [Display(Name = "شناسه")]
        public int WalletId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        [MinLength(2, ErrorMessage = "حداقل طول مجاز ۲ کاراکتر است")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز ۵۰ کاراکتر است")]
        public string WalletName { get; set; }

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
