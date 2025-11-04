using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("ExamResourceOrder")]
    public class ExamResourceOrder
    {
        [Key]
        public int ExamResourceOrderId { get; set; }

        [Required]
        public int ApplicantId { get; set; }

        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }

        [Required]
        public int ExamResourceId { get; set; }

        [ForeignKey("ExamResourceId")]
        public ExamResource ExamResource { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal TotalPrice { get; set; }

        //1 -> درحال بررسی
        //2 -> پرداخت شده
        //3 -> ارسال شده
        //4 -> تحویل داده شده
        //5 -> دانلود شده
        [Required]
        public short Status { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }
    }
}
