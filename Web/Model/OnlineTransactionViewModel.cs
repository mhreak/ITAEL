using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class OnlineTransactionViewModel
    {
        [Display(Name = "شناسه تراکنش")]
        public long OnlineTransactionId { get; set; }

        [Display(Name = "شناسه سفارش منبع")]
        public int? ExamResourceOrderId { get; set; }

        [Display(Name = "شناسه داوطلب")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int ApplicantId { get; set; }

        [Display(Name = "داوطلب")]
        public string? ApplicantFullName { get; set; }

        [Display(Name = "شناسه آگهی")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int JobAnnouncementId { get; set; }

        [Display(Name = "عنوان آگهی")]
        public string? JobAnnouncementTitle { get; set; }

        [Display(Name = "شناسه درگاه پرداخت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int BankGatewayId { get; set; }

        [Display(Name = "درگاه پرداخت")]
        public string BankGatewayName { get; set; }

        [Display(Name = "مبلغ")]
        [DisplayFormat(DataFormatString = "{0: #,0}")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public int Amount { get; set; }

        [Display(Name = "شماره مرجع")]
        [MaxLength(50, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۵۰ کاراکتر است")]
        public string ReferenceNumber { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public short State { get; set; }

        [Display(Name = "وضعیت")]
        public string StateStr { get; set; }

        [Display(Name = "شرح تراکنش")]
        [MaxLength(250, ErrorMessage = "حداکثر طول مجاز برای این فیلد ۲۵۰ کاراکتر است")]
        public string TransactionCmnt { get; set; }

        [Display(Name = "تاریخ")]
        [Required(ErrorMessage = "این فیلد الزامی است")]
        public DateTime InsertDate { get; set; }

        [Display(Name = "زمان انجام")]
        public string ShamsiInsertDate { get; set; }

        public DateTime? VerifyDate { get; set; }
    }
}
