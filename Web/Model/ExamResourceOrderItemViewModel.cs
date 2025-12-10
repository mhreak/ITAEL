using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class ExamResourceOrderItemViewModel
    {
        public int ExamResourceOrderItemId { get; set; }

        [Required]
        [Display(Name = "شناسه منبع آزمون")]
        public int ExamResourceId { get; set; }

        [Display(Name = "منبع آزمون")]
        public string? ResourceName { get; set; }

        [Required]
        [Display(Name = "شناسه سفارش منبع آزمون")]
        public int ExamResourceOrderId { get; set; }

        [Required]
        [Display(Name = "مبلغ")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "تاریخ ثبت")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string? ShamsiInsertDate { get; set; }
    }
}
