using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class WalletCommissionViewModel
    {
        [Display(Name = "شناسه")]
        public int WalletCommissionId { get; set; }

        [Display(Name = "شناسه کیف پول")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int WalletId { get; set; }

        [Display(Name = "کیف پول")]
        public string WalletName { get; set; }

        [Display(Name = "کارمزد")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int Commission { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }
    }
}
