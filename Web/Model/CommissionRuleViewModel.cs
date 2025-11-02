using System.ComponentModel.DataAnnotations;
using System;

namespace Web.Model
{
    public class CommissionRuleViewModel
    {
        [Display(Name = "شناسه")]
        public int CommissionRuleId { get; set; }

        // 1 ==> تعداد ثبت نام
        // 2 ==> مبلغ ثبت نام
        [Display(Name = "محاسبه بر اساس")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short CommissionBasedOn { get; set; }

        [Display(Name = "محاسبه بر اساس")]
        public string CommissionBasedOnStr { get; set; }

        [Display(Name = "کمترین مبلغ")]
        public long? MinimumAmount { get; set; }

        [Display(Name = "بیشترین مبلغ")]
        public long? MaximumAmount { get; set; }

        [Display(Name = "کمترین تعداد")]
        public int? MinimumNumber { get; set; }

        [Display(Name = "بیشترین تعداد")]
        public int? MaximumNumber { get; set; }

        // 1 ==> پورسانت درصدی
        // 2 ==> پورسانت به ازای هر ثبت نام
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short CommissionType { get; set; }

        [Display(Name = "نوع")]
        public string CommissionTypeStr { get; set; }

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int Value { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public bool Active { get; set; }

        [Display(Name = "وضعیت")]
        public string ActiveStr { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string ShamsiInsertDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Display(Name = "کمترین تعداد/مبلغ")]
        public string Min { get; set; }

        [Display(Name = "بیشترین تعداد/مبلغ")]
        public string Max { get; set; }
    }
}
