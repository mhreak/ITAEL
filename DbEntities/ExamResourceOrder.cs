using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("ExamResourceOrder")]
    public class ExamResourceOrder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ApplicantId { get; set; }

        [ForeignKey("ApplicantId")]
        public Applicant Applicant { get; set; }

        [Required]
        public int ExamResourceId { get; set; }

        [ForeignKey("ExamResourceId")]
        public ExamResource ExamResource { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal TotalPrice { get; set; }

        //1 -> Pending
        //2 -> Paid
        //3 -> Shipped
        //4 -> Delivered
        //5 -> Downloaded
        [StringLength(50)]
        [Required]
        public short Status { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime? DeliveryDate { get; set; }
    }
}
