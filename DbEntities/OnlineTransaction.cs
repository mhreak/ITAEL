namespace DbEntities
{
    [Table(name: "OnlineTransaction")]
    public class OnlineTransaction
    {
        [Key]
        public long OnlineTransactionId { get; set;}

        [Required]
        public int ApplicantId { get; set; }

        [Required]
        public int JobAnnouncementId { get; set; }

        public Applicant_JobAnnouncement Applicant_JobAnnouncement { get; set; }

        public int? ExamResourceOrderId { get; set; }

        [ForeignKey("ExamResourceOrderId")]
        public virtual ExamResourceOrder ExamResourceOrder { get; set; }

        [Required]
        public int BankGatewayId { get; set; }

        [ForeignKey("BankGatewayId")]
        public virtual BankGateway BankGateway { get; set; }

        [Required]
        public int Amount { get; set; }

        [MaxLength(50)]
        public string ReferenceNumber { get; set; }

        [Required]
        // -2 ==> انصراف از پرداخت
        // -1 ==> ناموفق
        //  0 ==> نامشخص
        //  1 ==> موفق
        public short State { get; set; }

        [MaxLength(250)]
        public string TransactionCmnt { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        public DateTime? VerifyDate { get; set; }
    }
}
