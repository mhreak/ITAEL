using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DbEntities;

namespace Web.Model
{
    public class ExamResourceOrderViewModel
    {
        [Display(Name = "شناسه سفارش منبع آزمون")]
        public int ExamResourceOrderId { get; set; }

        [Display(Name = "شناسه داوطلب")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int ApplicantId { get; set; }

        [Display(Name = "نام داوطلب")]
        public string? ApplicantFullName { get; set; }

        [Display(Name = "قیمت کلی")]
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public decimal TotalPrice { get; set; }

        //1 -> درحال بررسی
        //2 -> پرداخت شده
        //3 -> ارسال شده
        //4 -> تحویل داده شده
        //5 -> دانلود شده
        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short Status { get; set; }

        [Display(Name = "وضعیت")]
        public string? StatusStr { get; set; }

        [Display(Name = "تاریخ سفارش")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "تاریخ سفارش")]
        public string? ShamsiOrderDate { get; set; }

        [Display(Name = "تاریخ ارسال")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "تاریخ ارسال")]
        public string? ShamsiDeliveryDate { get; set; }

        public List<ExamResourceOrderItemViewModel> ExamResourceOrderItemViewModelList { get; set; } = [];
    }
}
