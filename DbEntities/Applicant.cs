namespace DbEntities
{
    [Table(name: "Applicant")]
    public class Applicant
    {
        [Key]
        public int ApplicantId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [StringLength(10)]
        public string? NationalCode { get; set; }

        [Required]
        public bool Gender { get; set; }

        [MaxLength(11)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(11)]
        public string Mobile { get; set; }

        public int? StudyFieldId { get; set; }

        [ForeignKey("StudyFieldId")]
        public virtual StudyField StudyField { get; set; }

        public int? CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }

        [StringLength(10)]
        public string? PostalCode { get; set; }

        [MaxLength(2000)]
        public string? Address { get; set; }

        public DateTime? BirthDate { get; set; }

        //1 -> عالی
        //2 -> متوسط
        //3 -> ضعیف
        public short? EnglishLanguageLevel { get; set; }

        [MaxLength(50)]
        public string? PersonalImageFileName { get; set; }

        [MaxLength(50)]
        public string? NationalCardFrontFileName { get; set; }

        [MaxLength(50)]
        public string? NationalCardBackFileName { get; set; }

        [MaxLength(50)]
        public string? IdentityCertificateFirstPageFileName { get; set; }

        [MaxLength(50)]
        public string? IdentityCertificateSecondPageFileName { get; set; }

        [MaxLength(50)]
        public string? EducationalCertificateFileName { get; set; }

        [Required]
        public DateTime InsertDate { get; set; }

        public virtual ICollection<ExamResourceOrder> ExamResourceOrderList { get; set; } = [];
        public virtual ICollection<ApplicantExamAttempt> ApplicantExamAttempt { get; set; } = [];
        public virtual ICollection<InterviewAppointment> InterviewAppointmentList { get; set; } = [];
    }
}
