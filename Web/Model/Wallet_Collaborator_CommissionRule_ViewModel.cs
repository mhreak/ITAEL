using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class Wallet_Collaborator_CommissionRule_ViewModel
    {
        [Display(Name = "شناسه کیف پول")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int WalletId { get; set; }

        [Display(Name = "کیف پول")]
        public string WalletName { get; set; }

        [Display(Name = "شناسه همکار")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int CollaboratorId { get; set; }

        [Display(Name = "کد ارجاع")]
        public string? ReferralCode { get; set; }

        [Display(Name = "شناسه قانون کارمزد")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int CommissionRuleId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }
    }
}
